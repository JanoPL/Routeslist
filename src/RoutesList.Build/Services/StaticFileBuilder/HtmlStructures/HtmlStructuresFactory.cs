using System.Text;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures
{
    /// <summary>
    /// Factory class responsible for creating HTML structure components.
    /// </summary>
    public class HtmlStructuresFactory : IHtmlStructuresFactory
    {
        /// <summary>
        /// Creates a new table structure instance.
        /// </summary>
        /// <param name="sb">The StringBuilder instance used for HTML generation.</param>
        /// <returns>An implementation of ITableStructure for building HTML tables.</returns>
        public ITableStructure CreateTableStructures(StringBuilder sb)
        {
            return new TableStructure(sb);
        }
    }
}
