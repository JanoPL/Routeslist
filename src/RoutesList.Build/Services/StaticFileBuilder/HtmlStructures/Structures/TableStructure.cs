using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    public class TableStructure : ITableStructure
    {
        public string TableRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TableColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TableData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private StringBuilder _stringBuilder;

        public TableStructure (StringBuilder sb)
        {
            _stringBuilder = sb;
        }

        public void Build()
        {
            throw new NotImplementedException();
        }
    }
}
