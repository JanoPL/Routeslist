using System.Text;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures
{
    public class HtmlStructuresFactory : IHtmlStructuresFactory
    {
        public ITableStructure CreateTableStructures(StringBuilder sb)
        {
            return new TableStructure(sb);
        }
    }
}
