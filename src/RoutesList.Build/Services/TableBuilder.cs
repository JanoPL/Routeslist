using ConsoleTables;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Interfaces;
using RoutesList.Build.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RoutesList.Services
{
    public class TableBuilder : ITableBuilder
    {
        private IRoutes _routes;
        private IBuilder _builder;
        private IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public ConsoleTable Table = new ConsoleTable("Method", "Uri", "Controller Name", "Action", "Full Name");
        private IList<RoutesInformationModel> ListRoutes { get; set; } = new List<RoutesInformationModel>();

        public TableBuilder(
            IActionDescriptorCollectionProvider collectionProvider, IRoutes routes, IBuilder builder
        ) {
            _actionDescriptorCollectionProvider = collectionProvider;
            _routes = routes;
            _builder = builder;
        }

        public async Task<string> AsyncGenerateTable(bool toJson = false, RoutesListOptions options = null)
        {
            Table.Rows.Clear();

            ListRoutes = _routes.getRoutesInformation(_actionDescriptorCollectionProvider);

            if (toJson) {
                string serialize = JsonConvert.SerializeObject(ListRoutes);

                return await Task.FromResult(serialize);
            }

            foreach (var route in ListRoutes) {
                Table.AddRow(route.Method_name, route.Template, route.Controller_name, route.Action_name, route.Display_name);
            }

            _builder.Build(Table, options);

            return await Task.FromResult(_builder.Result);
        }
    }
}
