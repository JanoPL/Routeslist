using ConsoleTables;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Interfaces;
using RoutesList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutesList.Services
{
    public class TableBuilder : ITableBuilder
    {
        private IRoutes _routes;
        private IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public ConsoleTable Table = new ConsoleTable("Method", "Uri", "Controller Name", "Action", "Full Name");
        private IList<RoutesInformationModel> ListRoutes { get; set; }

        public TableBuilder(IActionDescriptorCollectionProvider collectionProvider, IRoutes routes)
        {
            _actionDescriptorCollectionProvider = collectionProvider;
            _routes = routes;
        }

        public async Task<string> AsyncGenerateTable()
        {
            Table.Rows.Clear();

            ListRoutes = _routes.getRoutesInformation(_actionDescriptorCollectionProvider);

            foreach(var route in ListRoutes) {
                Table.AddRow(route.Method_name, route.Template, route.Controller_name, route.Action_name, route.Display_name);
            }

            return await Task.FromResult(Table.ToString());
        }
    }
}
