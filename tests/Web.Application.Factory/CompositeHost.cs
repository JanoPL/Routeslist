using Microsoft.Extensions.Hosting;

namespace Web.Application.Factory
{
    public class CompositeHost : IHost
    {
        private readonly IHost _testHost;
        private readonly IHost _kestrelHost;
        public IServiceProvider Services => _testHost.Services;


        public CompositeHost(IHost testhost, IHost kestrelHost)
        {
            _testHost = testhost;
            _kestrelHost = kestrelHost;
        }

        public void Dispose()
        {
            _testHost.Dispose();
            _kestrelHost.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await _testHost.StartAsync(cancellationToken).ConfigureAwait(false);
            await _kestrelHost.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await _testHost.StopAsync(cancellationToken).ConfigureAwait(false);
            await _kestrelHost.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
