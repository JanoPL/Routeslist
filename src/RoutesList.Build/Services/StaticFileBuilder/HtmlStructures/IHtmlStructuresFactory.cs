using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures
{
    public interface IHtmlStructuresFactory
    {
        ITableStructure CreateTableStructures(StringBuilder sb);
    }
}
