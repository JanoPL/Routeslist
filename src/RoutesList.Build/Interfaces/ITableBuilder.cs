using System.Threading.Tasks;
using RoutesList.Build.Models;

namespace RoutesList.Interfaces
{
    public interface ITableBuilder
    {
        Task<string> AsyncGenerateTable(bool toJson = false, RoutesListOptions options = null);
    }
}
