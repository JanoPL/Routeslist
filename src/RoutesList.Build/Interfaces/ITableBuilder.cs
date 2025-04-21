using System.Threading.Tasks;
using RoutesList.Build.Models;

namespace RoutesList.Build.Interfaces
{
    /// <summary>
    /// Interface for building tables from route data with various output options.
    /// </summary>
    public interface ITableBuilder
    {
        /// <summary>
        /// Generates a table asynchronously using the specified options.
        /// </summary>
        /// <param name="options">The options to configure table generation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated table as a string.</returns>
        Task<string> AsyncGenerateTable(RoutesListOptions options);
    
        /// <summary>
        /// Generates a table asynchronously with optional JSON output.
        /// </summary>
        /// <param name="isJson">If true, generates JSON output; otherwise, generates standard table format.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated table as a string.</returns>
        Task<string> AsyncGenerateTable(bool isJson);
    
        /// <summary>
        /// Generates a table asynchronously with specified JSON output preference and options.
        /// </summary>
        /// <param name="isJson">If true, generates JSON output; otherwise, generates standard table format.</param>
        /// <param name="options">The options to configure table generation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated table as a string.</returns>
        Task<string> AsyncGenerateTable(bool isJson, RoutesListOptions options);
    }
}
