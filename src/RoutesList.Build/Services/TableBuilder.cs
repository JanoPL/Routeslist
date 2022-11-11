using ConsoleTables;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Interfaces;
using RoutesList.Build.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using RoutesList.Build.Enums;
using RoutesList.Build.Extensions;
using System.Linq;

namespace RoutesList.Services
{
    public class TableBuilder : ITableBuilder
    {
        private IRoutes _routes;
        private IBuilder _builder;
        private IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private IList<RoutesInformationModel> ListRoutes { get; set; } = new List<RoutesInformationModel>();

        public TableBuilder(
            IActionDescriptorCollectionProvider collectionProvider, IRoutes routes, IBuilder builder
        ) {
            _actionDescriptorCollectionProvider = collectionProvider;
            _routes = routes;
            _builder = builder;
        }

        public Task<string> AsyncGenerateTable(RoutesListOptions options, bool toJson)
        {
            return GenerateTable(options, toJson);
        }

        public Task<string> AsyncGenerateTable(RoutesListOptions options)
        {
            return GenerateTable(options, false);
        }

        public Task<string> AsyncGenerateTable(bool json)
        {
            return GenerateTable(null, true);
        }

        private bool IsControllerActionDescriptor()
        {
            List<bool> result = new List<bool>();
            foreach (var route in ListRoutes) {
                result.Add(route.IsCompiledPageActionDescriptor == false);
            }

            return result.TrueForAll(x => x);
        }

        private bool IsCompiledPageActionDescriptor()
        {
            List<bool> result = new List<bool>();
            foreach (var route in ListRoutes) {
                result.Add(route.IsCompiledPageActionDescriptor == true);
            }

            return result.TrueForAll(x => x);
        }

        private async Task<string> GenerateTable(RoutesListOptions options, bool isJson)
        {
            ConsoleTable table = new ConsoleTable();
            IList<string> headers = new List<string>();
            ListRoutes = _routes.getRoutesInformation(_actionDescriptorCollectionProvider);

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

            if (!String.IsNullOrEmpty(ListRoutes[0].ViewEnginePath) || !String.IsNullOrEmpty(ListRoutes[0].RelativePath)) {
                foreach (var route in ListRoutes) {
                    string linkString = $"<a href=/{route.ViewEnginePath}>{route.ViewEnginePath ?? "/"} </a>";
                    table.AddRow(route.Display_name, /*route.ViewEnginePath*/ linkString, route.RelativePath);
                }
            }

            if (!string.IsNullOrEmpty(ListRoutes[0].Controller_name)) {
                foreach (var route in ListRoutes) {
                    string linkString = $"<a href=/{route.Template}>{route.Template ?? "/"} </a>";
                    table.AddRow(route.Method_name, linkString, route.Controller_name, route.Action_name, route.Display_name);
                }
            }

            _builder.Build(table, options);

            return await Task.FromResult(_builder.Result);
        }
    }
}
