using System;
using System.IO;
using System.Text;
using ConsoleTables;
using RoutesList.Build.Models;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// A builder class responsible for generating static HTML file content from route data.
    /// Implements the <see cref="IBuilder"/> interface.
    /// </summary>
    public class Builder : IBuilder
    {
        readonly StringBuilder _stringBuilder;
        RoutesListOptions _options;
        IndexCompiler _indexCompiler;
        private string BodyContent { get; set; }
        /// <summary>
        /// Gets or sets the final HTML content after the build process is complete.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// A builder class responsible for generating static HTML file content from route data.
        /// Implements the <see cref="IBuilder"/> interface.
        /// </summary>
        public Builder()
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.index.html");

            if (stream == null) {
                throw new FileNotFoundException("something wrong with index.html");
            }

            _stringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
        }
        /// <summary>
        /// Builds the HTML head section of the document.
        /// </summary>
        private void BuildHead()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            }

            _indexCompiler.CompileIndex(true);
        }

        /// <summary>
        /// Builds the HTML body section of the document using the provided body content.
        /// </summary>
        private void BuildBody()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            } else {
                _indexCompiler.BodyContent = BodyContent;
            }

            _indexCompiler.CompileIndex(false, true);
        }

        /// <summary>
        /// Builds the meta information section of the HTML document.
        /// </summary>
        private void BuildMeta()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds the footer section of the HTML document.
        /// </summary>
        private void BuildFooter()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            }

            _indexCompiler.CompileIndex(false, false, true);
        }

        /// <summary>
        /// Builds the class-specific section of the HTML document.
        /// </summary>
        private void BuildClass()
        {
            _indexCompiler ??= new IndexCompiler(_stringBuilder, _options);

            _indexCompiler.CompileIndex(false, false, false, true);
        }

#nullable enable
        /// <summary>
        /// Builds the complete HTML content using the provided table data and options.
        /// </summary>
        /// <param name="table">The console table containing route data to be rendered.</param>
        /// <param name="options">Configuration options for the routes list generation.</param>
        public void Build(ConsoleTable? table, RoutesListOptions? options)
        {
            if (table == null || options == null) {
                return;
            }

            _options = options;

            var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.TablePartialView.html");

            if (stream == null) {
                throw new FileNotFoundException("something wrong with TablePartialView.html");
            }

            var tableStringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());

            //Html structures factory
            HtmlData.GetInstance();

            HtmlData.Add<ConsoleTable>(table);

            BodyContent = HtmlStructureBodyCreator(new HtmlStructuresFactory(), tableStringBuilder);
            

            BuildHead();
            BuildBody();
            BuildFooter();
            BuildClass();

            Result = _stringBuilder.ToString();
        }

        /// <summary>
        /// Creates the HTML structure for the body content using the provided factory and table data.
        /// </summary>
        /// <param name="factory">The factory for creating HTML structures.</param>
        /// <param name="tableStringBuilder">The string builder containing table template.</param>
        /// <returns>The generated HTML table content as a string.</returns>
        private string HtmlStructureBodyCreator(
            IHtmlStructuresFactory factory,
            StringBuilder tableStringBuilder
        )
        {
            var htmlTable = factory.CreateTableStructures(tableStringBuilder);

            htmlTable.Build();

            return htmlTable.TableData;
        }
#nullable disable
    }
}
