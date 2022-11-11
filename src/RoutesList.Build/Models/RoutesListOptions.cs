using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Build.Models
{
    public class RoutesListOptions
    {
        public string Tittle { get; set; } = "RoutesList";
        public string CharSet { get; set; } = "UTF-8";
        public string FooterLink { get; set; } = "https://github.com/JanoPL/Routeslist";
        public string FooterText { get; set; } = "RoutesList";
        
        private dynamic _classes = "table";
        public dynamic GetClasses()
        {
            return _classes;
        }

        public void SetClasses(dynamic value)
        {
            _classes = value switch {
                string => (string)value,
                string[] => (string[])value,
                null => new string[] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}"),
            };
        }
    }
}
