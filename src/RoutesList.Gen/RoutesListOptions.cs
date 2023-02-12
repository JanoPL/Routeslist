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
#if NET6_0_OR_GREATER
            _tableClasses = value switch {
                string => (string)value,
                string[] => (string[])value,
                null => new string[] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}"),
            };
#endif

#if NETCOREAPP3_1 || NET5_0
            var type = _tableClasses.GetType();

            switch (type) {
                case string s when s.GetType() == type:
                    _tableClasses = (string)value;
                    break;
                case string[] sl when sl.GetType() == type:
                    _tableClasses = (string[])value;
                    break;
                case null:
                    _tableClasses = new string[] { "table" };
                    break;
                default:
                    throw new RuntimeBinderException($"It should be one of type string of string[], you provide: {value.GetType()}");
            }
#endif
        }
    }
}
