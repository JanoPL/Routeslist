using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;
using System;
using System.Collections.Generic;
using System.Text;

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
