using System.Text;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures
{
    /// <summary>
    /// Factory interface for creating HTML structure components.
    /// </summary>
    public interface IHtmlStructuresFactory
    {
        /// <summary>
        /// Creates a table structure using the provided StringBuilder instance.
        /// </summary>
        /// <param name="sb">The StringBuilder instance to use for HTML generation.</param>
        /// <returns>An instance of ITableStructure for building HTML tables.</returns>
        ITableStructure CreateTableStructures(StringBuilder sb);
    }
}
