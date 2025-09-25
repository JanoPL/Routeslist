using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RoutesList.Build.Enums;
using RoutesList.Build.Extensions;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Models;
using RoutesList.Build.Services.StaticFileBuilder;

namespace RoutesList.Build.Services
{
    /// <summary>
    /// Builds and formats route tables for ASP.NET Core applications.
    /// Handles both controller-based and page-based routes.
    /// </summary>
    public class TableBuilder : ITableBuilder
    {
        private readonly IRoutes _routes;
        private readonly IBuilder _builder;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        private IList<RoutesInformationModel> ListRoutes { get; set; } = new List<RoutesInformationModel>();

        /// <summary>
        /// Initializes a new instance of the TableBuilder class.
        /// </summary>
        /// <param name="collectionProvider">Provider for accessing action descriptors</param>
        /// <param name="routes">Service for retrieving route information</param>
        /// <param name="builder">Service for building output</param>
        public TableBuilder(
            IActionDescriptorCollectionProvider collectionProvider,
            IRoutes routes,
            IBuilder builder
        )
        {
            _actionDescriptorCollectionProvider = collectionProvider;
            _routes = routes;
            _builder = builder;
        }

        /// <summary>
        /// Asynchronously generates a table of routes using the specified options.
        /// </summary>
        /// <param name="options">Options for customizing the route list generation</param>
        /// <returns>A string representation of the generated table</returns>
        public Task<string> AsyncGenerateTable(RoutesListOptions options)
        {
            return GenerateTable(options);
        }

        /// <summary>
        /// Asynchronously generates a table of routes with optional JSON formatting.
        /// </summary>
        /// <param name="json">If true, outputs routes in JSON format</param>
        /// <returns>A string representation of the routes</returns>
        public Task<string> AsyncGenerateTable(bool json)
        {
            return GenerateTable(json, null);
        }

        /// <summary>
        /// Asynchronously generates a table of routes with JSON formatting and custom options.
        /// </summary>
        /// <param name="isJson">If true, outputs routes in JSON format</param>
        /// <param name="options">Options for customizing the route list generation</param>
        /// <returns>A string representation of the routes</returns>
        public Task<string> AsyncGenerateTable(bool isJson, RoutesListOptions options)
        {
            return GenerateTable(isJson, options);
        }

        /// <summary>
        /// Determines if all routes in the list are controller action descriptors.
        /// </summary>
        /// <returns>True if all routes are controller actions, false otherwise</returns>
        private bool IsControllerActionDescriptor()
        {
            List<bool> result = ListRoutes.Select(route => route.IsCompiledPageActionDescriptor == false).ToList();

            return result.TrueForAll(x => x);
        }

        /// <summary>
        /// Determines if all routes in the list are compiled page action descriptors.
        /// </summary>
        /// <returns>True if all routes are compiled pages, false otherwise</returns>
        private bool IsCompiledPageActionDescriptor()
        {
            List<bool> result = ListRoutes.Select(route => route.IsCompiledPageActionDescriptor).ToList();

            return result.TrueForAll(x => x);
        }

        /// <summary>
        /// Generates a formatted table of routes using the specified options.
        /// </summary>
        /// <param name="options">Options for customizing the route list generation</param>
        /// <returns>A string representation of the generated table</returns>
        private async Task<string> GenerateTable(RoutesListOptions options)
        {
#if NET5_0_OR_GREATER
            ConsoleTable table = new();
#endif
#if NETCOREAPP3_1
            ConsoleTable table = new ConsoleTable();
#endif
            _routes.SetAssembly(options.AppAssembly);
            ListRoutes = _routes.GetRoutesInformation(_actionDescriptorCollectionProvider);

            table = BuildHeaders(table);

            table = BuildRows(table);

            _builder.Build(table, options);

            return await Task.FromResult(_builder.Result);
        }

#nullable enable
        private async Task<string> GenerateTable(bool isJson, RoutesListOptions? options)
        {
            if (options != null) {
                _routes.SetAssembly(options.AppAssembly);
            }

            ListRoutes = _routes.GetRoutesInformation(_actionDescriptorCollectionProvider);

            if (!isJson)
            {
                return await Task.FromResult("");
            }
            
            var serialize = new StringBuilder();

            if (IsCompiledPageActionDescriptor()) {
                serialize.AppendLine(JsonConvert.SerializeObject(
                    ListRoutes.Select(x => {
                        return new {
                            x.RelativePath,
                            x.ViewEnginePath,
                            x.DisplayName,
                            x.Template,
                        };
                    })
                ));
            }

            if (IsControllerActionDescriptor()) {
                serialize.AppendLine(JsonConvert.SerializeObject(
                    ListRoutes.Select(x => {
                        return new {
                            x.DisplayName,
                            x.ControllerName,
                            x.Template,
                            x.ActionName,
                            x.MethodName,
                        };
                    })
                ));
            }

            return await Task.FromResult(serialize.ToString());

        }
#nullable disable

        /// <summary>
        /// Builds the header row of the routes table based on route types.
        /// </summary>
        /// <param name="table">The console table to add headers to</param>
        /// <returns>The console table with headers added</returns>
        private ConsoleTable BuildHeaders(ConsoleTable table)
        {
            IList<string> headers = new List<string>();

            if (ListRoutes.Count <= 0)
            {
                return table;
            }
            
            if (!String.IsNullOrEmpty(ListRoutes[0].ViewEnginePath) || !String.IsNullOrEmpty(ListRoutes[0].RelativePath)) {
                foreach (var headerName in EnumExtension.GetListOfDescription<TableHeaderPageActionDescriptor>()) {
                    headers.Add(headerName);
                }

                table.AddColumn(headers);
            }

            if (!String.IsNullOrEmpty(ListRoutes[0].ControllerName)) {
                foreach (var headerName in EnumExtension.GetListOfDescription<TableHeaderControllerActionDescriptor>()) {
                    headers.Add(headerName);
                }

                table.AddColumn(headers);
            }


            return table;
        }

        /// <summary>
        /// Builds the data rows of the routes table.
        /// </summary>
        /// <param name="table">The console table to add rows to</param>
        /// <returns>The console table with rows added</returns>
        private ConsoleTable BuildRows(ConsoleTable table)
        {
            if (ListRoutes.Count <= 0) return table;
            
            foreach (RoutesInformationModel route in ListRoutes) {
                AddTableRow(ref table, route);
            }

            return table;
        }

        /// <summary>
        /// Adds a single route row to the table.
        /// </summary>
        /// <param name="table">The console table to add the row to</param>
        /// <param name="route">The route information to add as a row</param>
        private void AddTableRow (ref ConsoleTable table, RoutesInformationModel route)
        {
            if (route.IsCompiledPageActionDescriptor) {
                string linkString = $"<a href=/{route.ViewEnginePath}>{route.ViewEnginePath ?? route.Template ?? "/"} </a>";

                if (table.Columns.Count > 3) {
                    table.AddRow(null, linkString, route.DisplayName, null, route.RelativePath);
                } else {
                    table.AddRow(route.DisplayName, linkString, route.RelativePath);
                }
            }

            if (!route.IsCompiledPageActionDescriptor) {
                string linkString = $"<a href=/{route.Template}>{route.Template ?? "/"} </a>";
                table.AddRow(route.MethodName, linkString, route.ControllerName, route.ActionName, route.DisplayName);
            }
        }
    }
}
