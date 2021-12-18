using ConsoleTables;
using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public interface IBuilder
    {
        void Build(ConsoleTable table, RoutesListOptions options);
        string Result { get; set; }
    }
}
