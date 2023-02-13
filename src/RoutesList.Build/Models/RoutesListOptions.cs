using System.Diagnostics.CodeAnalysis;
using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Build.Models
{
    public class RoutesListOptions
    {
        public string Tittle { get; set; } = "RoutesList";
        public string CharSet { get; set; } = "UTF-8";
        public string FooterLink { get; set; } = "https://github.com/JanoPL/Routeslist";
        public string FooterText { get; set; } = "RoutesList";
        public string Description { get; set; } = "Routing debugger for DotNet Core applications. A list of all routes in the formatted table";
        private dynamic _classes { get; set; } = "table";

        public dynamic GetClasses()
        {
            return _classes;
        }

#if NET5_0_OR_GREATER
        public void SetClasses(dynamic value)
        {
            _classes = value switch {
                string => (string)value,
                string[] => (string[])value,
                null => new string[] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}"),
            };
        }
#endif

#if NETCOREAPP3_1
        public void SetClasses(dynamic value)
        {
            if (value is null) {
                _classes = new string[] { "table" };
                return;
            }

            if (value is string @string) {
                _classes = @string;
                return;
            }

            if (value is string[] array) {
                _classes = array;
                return;
            }

            throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}");
        }
#endif 
    }
}
