using RoutesList_cli.ProcessUtils;
using System.Collections.Generic;
using System.Text;

namespace RoutesList_cli.Internal
{
    internal static class CommandLineArguments
    {
        public static IEnumerable<string> DotnetArgument(ProcessSpec processSpec)
        {
            List<string> arguments = new List<string>(processSpec.Arguments);

            if (processSpec.EnvironmentVariables["DOTNET_MSBUILD"] == "1")
            {
                arguments.Add("msbuild");
            }

            return arguments;
        }

        public static string ConcateArgument(ProcessSpec processSpec)
        {

            List<string> argumentList = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var env in processSpec.EnvironmentVariables)
            {
                argumentList.Add(ProcessSingleArg(env));
            }

            foreach (var item in argumentList)
            {
                if (stringBuilder.Length == 0)
                {
                    stringBuilder.Append(item);
                } else
                {
                    stringBuilder.Append(" " + item);
                }
            }

            return stringBuilder.ToString();
        }

        private static string ProcessSingleArg(KeyValuePair<string, string> arg)
        {
            if (arg.Key == "DOTNET_MSBUILD")
            {
                return "msbuild";
            }

            if (arg.Key == "DOTNET_HELP")
            {
                return "-help";
            }

            if (arg.Key == "DOTNET_VERBOSE" && arg.Key != "DOTNET_HELP")
            {
                return "/v:d";
            }

            return string.Empty;
        }
    }
}
