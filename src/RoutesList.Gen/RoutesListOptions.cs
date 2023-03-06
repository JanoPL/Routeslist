using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Gen
{
    public class RoutesListOptions
    {
        public string Tittle { get; set; } = "RoutesList";
        public string Endpoint { get; set; } = "routes";

        private dynamic _tableClasses { get; set; } = "table";

        private Assembly _appAssembly { get; set; }

        public dynamic GetTableClasses()
        {
            return _tableClasses;
        }

        public void SetAppAssembly(Assembly assembly)
        {
            _appAssembly = assembly;
        }

        public Assembly GetAppAssembly()
        {
            return _appAssembly;
        }

#if NET5_0_OR_GREATER
        public void SetTableClasses(dynamic value)
        {
            _tableClasses = value switch {
                string => (string)value,
                string[] => (string[])value,
                null => new string[] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}"),
            };
        }
#endif

#if NETCOREAPP3_1
        public void SetTableClasses(dynamic value)
        {
            if (value is null) {
                _tableClasses = new string[] { "table" };
                return;
            }

            if (value is string @string) {
                _tableClasses = @string;
                return;
            }

            if (value is string[] @array) {
                _tableClasses = @array;
                return;
            }

            throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}");
        }
#endif
    }
}
