using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList_cli.ProcessUtils
{
    public class DotnetBuild
    {
        private readonly ProcessRunner processRunner;

        public DotnetBuild()
        {
            this.processRunner = new ProcessRunner();
        }

        public async void Build(ProcessSpec processSpec)
        {
            await this.processRunner.RunAsync(processSpec);
        }
    }
}
