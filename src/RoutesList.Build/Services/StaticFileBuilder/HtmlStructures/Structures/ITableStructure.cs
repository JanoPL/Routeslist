using System.Collections.Generic;

namespace RoutesList.Build.Services.StaticFileBuilder.HtmlStructures.Structures
{
    /// <summary>
    /// Interface defining the structure and behavior of a table in HTML format.
    /// </summary>
    public interface ITableStructure
    {
        /// <summary>
        /// Gets or sets the collection of table rows, where each row is represented as an array of objects.
        /// </summary>
        public IList<object[]> TableRow { get; set; }
        /// <summary>
        /// Gets or sets the collection of table columns, where each column is represented as an object.
        /// </summary>
        public IList<object> TableColumn { get; set; }
        /// <summary>
        /// Gets or sets the string representation of the table data.
        /// </summary>
        public string TableData { get; set; }
        /// <summary>
        /// Builds the table structure using the configured rows, columns, and data.
        /// </summary>
        public void Build();
    }
}
