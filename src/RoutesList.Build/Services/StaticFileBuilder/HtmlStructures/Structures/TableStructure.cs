using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    /// <summary>
    /// Handles the construction and formatting of HTML table structures.
    /// </summary>
    public class TableStructure : ITableStructure
    {

        private readonly StringBuilder _stringBuilder;
        /// <summary>
        /// Gets or sets the collection of table rows, where each row is an array of objects.
        /// </summary>
        public IList<object[]> TableRow { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of table column headers.
        /// </summary>
        public IList<object> TableColumn { get; set; }
        
        /// <summary>
        /// Gets or sets the final HTML table markup.
        /// </summary>
        public string TableData { get; set; } = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableStructure"/> class.
        /// </summary>
        /// <param name="sb">The StringBuilder instance used for constructing the table HTML.</param>
        public TableStructure(StringBuilder sb)
        {
            _stringBuilder = sb;
        }

        /// <summary>
        /// Builds the HTML table structure by processing column headers and row data.
        /// </summary>
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

        /// <summary>
        /// Generates HTML markup for table column headers.
        /// </summary>
        /// <returns>A string containing the HTML markup for table headers.</returns>
        private string GetTableColumnTag()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var tc in TableColumn) {
                string tag = "<th scope=\"col\">" + tc + "</th>";

                sb.AppendLine(tag);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates HTML markup for table row data.
        /// </summary>
        /// <returns>A string containing the HTML markup for table rows.</returns>
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
