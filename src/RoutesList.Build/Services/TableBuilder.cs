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
    public class TableBuilder : ITableBuilder
    {
        private readonly IRoutes _routes;
        private readonly IBuilder _builder;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        private IList<RoutesInformationModel> ListRoutes { get; set; } = new List<RoutesInformationModel>();

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

        public Task<string> AsyncGenerateTable(RoutesListOptions options)
        {
            return GenerateTable(options);
        }

        public Task<string> AsyncGenerateTable(bool json)
        {
            return GenerateTable(json, null);
        }

        public Task<string> AsyncGenerateTable(bool isJson, RoutesListOptions options)
        {
            return GenerateTable(isJson, options);
        }

        private bool IsControllerActionDescriptor()
        {
            List<bool> result = ListRoutes.Select(route => route.IsCompiledPageActionDescriptor == false).ToList();

            return result.TrueForAll(x => x);
        }

        private bool IsCompiledPageActionDescriptor()
        {
            List<bool> result = ListRoutes.Select(route => route.IsCompiledPageActionDescriptor).ToList();

            return result.TrueForAll(x => x);
        }

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

        private ConsoleTable BuildRows(ConsoleTable table)
        {
            if (ListRoutes.Count <= 0) return table;
            
            foreach (RoutesInformationModel route in ListRoutes) {
                AddTableRow(ref table, route);
            }

            return table;
        }

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
