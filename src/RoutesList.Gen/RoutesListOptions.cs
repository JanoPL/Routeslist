using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Gen
{
    public class RoutesListOptions
    {
        public string Tittle { get; set; } = "RoutesList";
        public string Endpoint { get; set; } = "routes";

        private dynamic _tableClasses = "table";

        public dynamic GetTableClasses()
        {
            return _tableClasses;
        }

        public void SetTableClasses(dynamic value)
        {
            _tableClasses = value switch {
                string => (string)value,
                string[] => (string[])value,
                null => new string[1] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}"),
            };
        }
    }
}
