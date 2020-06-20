using Microsoft.Extensions.Internal;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RoutesList_cli.ProcessUtils
{
    public class ProcessState : IDisposable
    {
        private readonly Process _process;
        private readonly TaskCompletionSource<object> _tcs = new TaskCompletionSource<object>();
        public Task Task { get; }
        private volatile bool _disposed;

        
        public ProcessState(Process process)
        {
            this._process = process;
            process.Exited += OnExited;
            Task = _tcs.Task.ContinueWith(_ =>
            {
                try
                {
                    if (!this._process.WaitForExit(Int32.MaxValue))
                    {
                        throw new TimeoutException();
                    }

                    process.WaitForExit();
                }
                catch (InvalidOperationException)
                {

                }
            });
        }

        private void OnExited(object sender, EventArgs args)
            => _tcs.TrySetResult(null);

        public void TryKill()
        {
            if(_disposed)
            {
                return;
            }

            try
            {
                if (!_process.HasExited)
                {
                    _process.KillTree();
                }
            } catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while killing process '{_process.StartInfo.FileName} {_process.StartInfo.Arguments}': {ex.Message}");

                #if DEBUG
                    Console.Error.WriteLine(ex.ToString());
                #endif
            }
        }
        public void Dispose()
        {
            if (_disposed)
            {
                TryKill();
                _disposed = true;
                _process.Exited -= OnExited;
                _process.Dispose();
            }
        }
    }
}
