using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    public interface ITableStructure
    {
        public string TableRow { get; set; }
        public string TableColumn { get; set; }
        public string TableData { get; set; }
        public void Build();
    }
}
