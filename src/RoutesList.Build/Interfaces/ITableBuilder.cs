using System.Threading.Tasks;
using RoutesList.Build.Models;

namespace RoutesList.Interfaces
{
    public interface ITableBuilder
    {
        Task<string> AsyncGenerateTable(RoutesListOptions options);
        Task<string> AsyncGenerateTable(bool json);
    }
}
