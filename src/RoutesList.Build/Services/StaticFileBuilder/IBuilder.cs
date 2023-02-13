using ConsoleTables;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public interface IBuilder
    {
        void Build(ConsoleTable table, RoutesListOptions options);
        string Result { get; set; }
    }
}
