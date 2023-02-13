using System.Collections.Generic;

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
