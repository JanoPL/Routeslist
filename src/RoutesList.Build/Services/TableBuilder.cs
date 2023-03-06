using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RoutesList.Build.Enums;
using RoutesList.Build.Extensions;
using RoutesList.Build.Models;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Interfaces;

namespace RoutesList.Services
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
            return GenerateTable(json);
        }

        private bool IsControllerActionDescriptor()
        {
#if NET5_0_OR_GREATER
            List<bool> result = new();
#endif
#if NETCOREAPP3_1
            List<bool> result = new List<bool>();
#endif

            foreach (var route in ListRoutes) {
                result.Add(route.IsCompiledPageActionDescriptor == false);
            }

            return result.TrueForAll(x => x);
        }

        private bool IsCompiledPageActionDescriptor()
        {
#if NET5_0_OR_GREATER
            List<bool> result = new();
#endif
#if NETCOREAPP3_1
            List<bool> result = new List<bool>();
#endif

            foreach (var route in ListRoutes) {
                result.Add(route.IsCompiledPageActionDescriptor == true);
            }

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
            _routes.SetAssembly(options.GetAppAssembly());
            ListRoutes = _routes.getRoutesInformation(_actionDescriptorCollectionProvider);

            table = BuildHeaders(table);

            table = BuildRows(table);

            _builder.Build(table, options);

            return await Task.FromResult(_builder.Result);
        }

        private async Task<string> GenerateTable(bool isJson)
        {
            ListRoutes = _routes.getRoutesInformation(_actionDescriptorCollectionProvider);

            if (isJson) {
                string serialize = String.Empty;

                if (IsCompiledPageActionDescriptor()) {
                    serialize = JsonConvert.SerializeObject(
                            ListRoutes.Select(x => {
                                return new {
                                    x.RelativePath,
                                    x.ViewEnginePath,
                                    x.Display_name,
                                };
                            })
                    );
                }

                if (IsControllerActionDescriptor()) {
                    serialize = JsonConvert.SerializeObject(
                        ListRoutes.Select(x => {
                            return new {
                                x.Display_name,
                                x.Controller_name,
                                x.Template,
                                x.Action_name,
                                x.Method_name,
                            };
                        })
                    );
                }

                return await Task.FromResult(serialize);
            }

            return await Task.FromResult("");
        }

        private ConsoleTable BuildHeaders(ConsoleTable table)
        {
            IList<string> headers = new List<string>();

            if (ListRoutes.Count > 0) {
                if (!String.IsNullOrEmpty(ListRoutes[0].ViewEnginePath) || !String.IsNullOrEmpty(ListRoutes[0].RelativePath)) {
                    foreach (var headerName in EnumExtension.GetListOfDescription<TableHeaderPageActionDescriptor>()) {
                        headers.Add(headerName);
                    }

                    table.AddColumn(headers);
                }

                if (!String.IsNullOrEmpty(ListRoutes[0].Controller_name)) {
                    foreach (var headerName in EnumExtension.GetListOfDescription<TableHeaderControllerActionDescriptor>()) {
                        headers.Add(headerName);
                    }

                    table.AddColumn(headers);
                }
            }


            return table;
        }

        private ConsoleTable BuildRows(ConsoleTable table)
        {
            if (ListRoutes.Count > 0) {
                if (!String.IsNullOrEmpty(ListRoutes[0].ViewEnginePath) || !String.IsNullOrEmpty(ListRoutes[0].RelativePath) || !string.IsNullOrEmpty(ListRoutes[0].Template)) {
                    foreach (var route in ListRoutes) {
                        string linkString = $"<a href=/{route.ViewEnginePath}>{route.ViewEnginePath ?? route.Template ?? "/"} </a>";
                        table.AddRow(route.Display_name, /*route.ViewEnginePath*/ linkString, route.RelativePath);
                    }
                }

                if (!string.IsNullOrEmpty(ListRoutes[0].Controller_name)) {
                    foreach (var route in ListRoutes) {
                        string linkString = $"<a href=/{route.Template}>{route.Template ?? "/"} </a>";
                        table.AddRow(route.Method_name, linkString, route.Controller_name, route.Action_name, route.Display_name);
                    }
                }
            }

            return table;
        }
    }
}
