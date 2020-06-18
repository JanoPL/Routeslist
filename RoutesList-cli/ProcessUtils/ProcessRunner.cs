using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace RoutesList_cli.ProcessUtils
{
    public class ProcessRunner
    {

        private Process CreateProcess(ProcessSpec processSpec)
        {
            var process = new System.Diagnostics.Process
            {
                EnableRaisingEvents = true,
                StartInfo =
                {
                    FileName = processSpec.Executable,
                    Arguments = "", //TODO add msbuild parameters to dotnet process
                    UseShellExecute = false,
                    WorkingDirectory = processSpec.WorkingDirectory,
                    RedirectStandardOutput = processSpec.IsOutputCaptured,
                    RedirectStandardError = processSpec.IsOutputCaptured
                }
            };

            foreach (var env in processSpec.EnvironmentVariables)
            {
                process.StartInfo.Environment.Add(env.Key, env.Value);
            }

            return process;
        }

        public async Task<int> RunAsync(ProcessSpec processSpec)
        {
            int exitCode;

            using (var process = CreateProcess(processSpec))
            using(var processState = new ProcessState(process))
            {
                process.OutputDataReceived += (_, a) =>
                {
                    if (!String.IsNullOrEmpty(a.Data))
                    {
                        processSpec.OutputCapture.AddLine(a.Data);
                    }
                };

                process.ErrorDataReceived += (_, a) =>
                {
                    if (!String.IsNullOrEmpty(a.Data))
                    {
                        processSpec.OutputCapture.AddLine(a.Data);
                    }
                };

                process.Start();

                if (processSpec.IsOutputCaptured)
                {
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                    await processState.Task;
                } else
                {
                    await processState.Task;
                }

                exitCode = process.ExitCode;
            }

            return exitCode;
        }
    }
}
