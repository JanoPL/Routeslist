using System.Threading.Tasks;
using RoutesList.Build.Models;

namespace RoutesList.Build.Interfaces
{
    public interface ITableBuilder
    {
        Task<string> AsyncGenerateTable(RoutesListOptions options);
        Task<string> AsyncGenerateTable(bool isJson);
        Task<string> AsyncGenerateTable(bool isJson, RoutesListOptions options);
    }
}
