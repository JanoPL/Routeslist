namespace RoutesList_cli.ProcessUtils
{
    public class DotnetBuild
    {
        private readonly ProcessRunner processRunner;

        public DotnetBuild()
        {
            this.processRunner = new ProcessRunner();
        }

        public async void Build(ProcessSpec processSpec, CommandLineOptions options = null)
        {
            processSpec.EnvironmentVariables["DOTNET_MSBUILD"] = "msbuild";

            if (options.isVerbose)
            {
                processSpec.EnvironmentVariables["DOTNET_VERBOSE"] = "1";
            }

            if (options.isHelp)
            {
                processSpec.EnvironmentVariables["DOTNET_HELP"] = "1";
            }

            await this.processRunner.RunAsync(processSpec);
        }
    }
}
