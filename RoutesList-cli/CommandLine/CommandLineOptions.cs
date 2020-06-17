using RoutesList_cli.Internal;
using System.Collections.Generic;

namespace RoutesList_cli
{
    public class CommandLineOptions
    {
        public string Project { get; private set; }
        public bool isHelp { get; private set; }
        
        public bool isVerbose { get; private set; }

        public IList<string> RemainingArguments { get; private set; }

        public static CommandLineOptions Parse(string[] args, string project)
        {
            bool verbose = false;
            bool help = false;
            List<string> remainingArguments = new List<string>();

            foreach (var item in args)
            {
                switch (item)
                {
                    case "--verbose":
                        verbose = true;
                        break;
                    case "--help":
                        help = true;
                        break;
                    default:
                        remainingArguments.Add(item);
                        break;
                }
            }

            string projectName = MsBuildProject.FindProjectName(project);

            var commandLineOptions = new CommandLineOptions
            {
                Project = projectName,
                isHelp = help,
                isVerbose = verbose, 
                RemainingArguments = remainingArguments
            };

            return commandLineOptions;
        }
    }
}
