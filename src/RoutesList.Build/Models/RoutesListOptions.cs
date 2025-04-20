#nullable enable
using System;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Build.Models
{
    /// <summary>
    /// Configuration options for RoutesList
    /// </summary>
    public class RoutesListOptions
    {
        /// <summary>
        /// Title of the routes list page
        /// </summary>
        public string Title { get; set; } = "RoutesList";

        /// <summary>
        /// Character set for the HTML page
        /// </summary>
        public string CharSet { get; set; } = "UTF-8";

        /// <summary>
        /// Link displayed in the footer
        /// </summary>
        public string FooterLink { get; set; } = "https://github.com/JanoPL/Routeslist";

        /// <summary>
        /// Text displayed in the footer
        /// </summary>
        public string FooterText { get; set; } = "RoutesList";

        /// <summary>
        /// Description of the routes list functionality
        /// </summary>
        public string Description { get; set; } = "Routing debugger for DotNet Core applications. A list of all routes in the formatted table";
        
        /// <summary>
        /// The URL endpoint path where the routes list will be accessible
        /// Default value is "routes" which means the list will be available at /routes
        /// </summary>
        public string Endpoint { get; set; } = "routes";

        private object _classes = "table";
        
        /// <summary>
        /// CSS classes for the table
        /// </summary>
        public object? Classes
        {
            get => _classes;
            set
            {
        #if NET5_0_OR_GREATER
                _classes = value switch {
                    string s => s,
                    string[] arr => arr,
                    null => new[] { "table" },
                    _ => throw new RuntimeBinderException($"It should be one of type string or string[], you provided: {value.GetType()}")
                };
        #else
                if (value is null) {
                    _classes = new[] { "table" };
                    return;
                }
        
                if (value is string stringValue) {
                    _classes = stringValue;
                    return;
                }
        
                if (value is string[] arrayValue) {
                    _classes = arrayValue;
                    return;
                }
        
                throw new RuntimeBinderException($"It should be one of type string or string[], you provided: {value.GetType()}");
        #endif
            }
        }

        /// <summary>
        /// Application assembly to scan for routes
        /// </summary>
        public Assembly? AppAssembly { get; set; }

        /// <summary>
        /// Gets the CSS classes for the table
        /// </summary>
        /// <returns>String or string array of CSS classes</returns>
        [Obsolete("use Classes property instead, this method will be removed in future versions")]
        public object? GetClasses()
        {
            return Classes;
        }

        /// <summary>
        /// Sets the application assembly
        /// </summary>
        /// <param name="assembly">Assembly to scan for routes</param>
        [Obsolete("Use AppAssembly property instead, this method will be removed in future versions, use AppAssembly property instead of this method")]
        public void SetAppAssembly(Assembly assembly)
        {
            AppAssembly = assembly;
        }

        /// <summary>
        /// Gets the application assembly
        /// </summary>
        /// <returns>The application assembly</returns>
        [Obsolete("Use AppAssembly property instead")]
        public Assembly? GetAppAssembly()
        {
            return AppAssembly;
        }

        /// <summary>
        /// Sets the CSS classes for the table
        /// </summary>
        /// <param name="value">String or string array of CSS classes</param>
        /// <exception cref="RuntimeBinderException">Thrown when the provided value is not a string or string array</exception>
        [Obsolete("Use Classes property instead, this method will be removed in future versions")]
        public void SetClasses(object? value)
        {
#if NET5_0_OR_GREATER
            _classes = value switch {
                string s => s,
                string[] arr => arr,
                null => new[] { "table" },
                _ => throw new RuntimeBinderException($"It should be one of type string or string[], you provided: {value.GetType()}")
            };
#else
            if (value is null) {
                Classes = new[] { "table" };
                return;
            }

            if (value is string stringValue) {
                Classes = stringValue;
                return;
            }

            if (value is string[] arrayValue) {
                Classes = arrayValue;
                return;
            }

            throw new RuntimeBinderException($"It should be one of type string or string[], you provided: {value.GetType()}");
#endif
        }
    }
}