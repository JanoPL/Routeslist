using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    public class TableStructure : ITableStructure
    {

        private readonly StringBuilder _stringBuilder;
        public IList<object[]> TableRow { get; set; }
        public IList<object> TableColumn { get; set; }
        public string TableData { get; set; } = String.Empty;

        public TableStructure (StringBuilder sb)
        {
            _stringBuilder = sb;
        }

        public void Build()
        {
            var tableData = HtmlData.GetData<ConsoleTable>();

            TableColumn = tableData.Columns;
            TableRow = tableData.Rows;

            var tableColumn = GetTableColumnTag();
            var tableRow = GetTableRowData();
            string theadTag = "$(thead-trow)";
            string tbodyTag = "$(tbody-trow-data)";

            _stringBuilder.Replace(theadTag, tableColumn);
            
            _stringBuilder.Replace(tbodyTag, tableRow);

            TableData = _stringBuilder.ToString();
        }

        private string GetTableColumnTag()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var tc in TableColumn) {
                string tag = "<th scope=\"col\">" + tc + "</th>";

                sb.AppendLine(tag);
            }

            return sb.ToString();
        }

        private string GetTableRowData()
        {

            StringBuilder sb = new StringBuilder();

            foreach (var row in TableRow) {
                sb.AppendLine("<tr>");

                foreach (var rowObject in row) {
                    string tag = "<td>" + (rowObject != null ? rowObject.ToString() : "empty") + "</td>";
                    sb.AppendLine(tag);
                }

                sb.AppendLine("</tr>");
            }

            return sb.ToString();
        }
    }
}
