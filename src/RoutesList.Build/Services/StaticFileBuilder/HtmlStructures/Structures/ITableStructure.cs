using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    public interface ITableStructure
    {
        public IList<object[]> TableRow { get; set; }
        public IList<object> TableColumn { get; set; }
        public string TableData { get; set; }
        public void Build();
    }
}
