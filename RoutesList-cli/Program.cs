using RoutesList_cli.Internal;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RoutesList_cli
{
    public class Program
    {
        private readonly string _workingDirectory;

        public Program(string workingDirectory)
        {
            this._workingDirectory = workingDirectory;
        }
        
        static void AppDescription()
        {
            var versionString = Assembly.GetEntryAssembly()
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion
                    .ToString();

            var descriptionString = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyDescriptionAttribute>()
                .Description
                .ToString();

            Console.WriteLine($"RouteList-cli \n version: {versionString}");
            Console.WriteLine($"Description \n {descriptionString}");
            Console.WriteLine("\n------------------");
            Console.WriteLine("\n Usage:");
            Console.WriteLine("routeslist-cli <project> <options>");
            Console.WriteLine("\n------------------");
        }

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                AppDescription();
                return 0;
            }

            try
            {
                var projectFile = MsBuildProject.FindProjectFile(args);

                var program = new Program(projectFile ?? Directory.GetCurrentDirectory());
                
                return program.Run(args, projectFile);
                
            } catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected error: ");
                Console.Error.WriteLine(ex.Message);
                return 1;
            }
        }

        private int Run(string[] args, string project)
        {
            int result;

            CommandLineOptions options;

            try
            {
                options = CommandLineOptions.Parse(args, project);

                #if DEBUG
                Debug.WriteLine(
                    $"ProjectName: {options.Project}," +
                    $" Help: {options.isHelp}," +
                    $" Verbose: {options.isVerbose}"
                    );
                #endif

                if (options == null)
                {
                    result = 1;
                } 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                result = 1;
            }

            try
            {
                //TODO Dotnet msbuild

                result = 0;
                
            } catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected error: ");
                Console.WriteLine(ex.Message);
                result = 1;
            }
            
            return result;
        }
    }
}
