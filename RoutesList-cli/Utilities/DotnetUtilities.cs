using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RoutesList_cli.Utilities
{
    public class DotnetUtilities
    {
        private const string Name = "dotnet";
        public static string DotnetPath { get; }

        static DotnetUtilities()
        {
            DotnetPath = TryFindPath() ?? null;
        }

        public static string DotnetPathOrDefault()
            => DotnetPath ?? Name;

        private static string TryFindPath()
        {
            
            var fileName = Name;

            //TODO add linux OSPlatform
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileName += ".exe";
            }

            var mainModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            if (
                !String.IsNullOrEmpty(mainModule?.FileName) 
                && Path.GetFileName(mainModule.FileName).Equals(fileName, StringComparison.OrdinalIgnoreCase)
            )
            {
                return mainModule.FileName;
            }

            return null;
        }
    }
}
