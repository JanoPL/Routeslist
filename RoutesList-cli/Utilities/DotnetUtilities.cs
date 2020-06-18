using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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
