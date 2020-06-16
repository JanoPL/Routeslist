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
            Console.WriteLine("routeslist-cli <command>");
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
                var program = new Program(Directory.GetCurrentDirectory());
                
                return program.Run(args);
                
            } catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected error: ");
                Console.Error.WriteLine(ex.ToString());
                return 1;
            }
        }

        private int Run(string[] args)
        {
            int result;
            if (args.Length > 0)
            {
                try
                {
                    Debug.WriteLine($"Directory: {this._workingDirectory}");

                    result = 0;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Unexpected error:");
                    Console.Error.WriteLine(ex.ToString());
                    result = 1;
                }
            } else
            {
                Console.Error.WriteLine("Parameters error:");
                Console.Error.WriteLine("Missing Parameters");
                result = 1;
            }

            return result;
        }
    }
}
