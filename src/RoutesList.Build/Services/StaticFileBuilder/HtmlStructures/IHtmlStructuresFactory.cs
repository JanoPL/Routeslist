using System.Text;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures
{
    public interface IHtmlStructuresFactory
    {
        ITableStructure CreateTableStructures(StringBuilder sb);
    }
}
