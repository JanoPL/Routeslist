using System;
using System.IO;
using System.Text;
using ConsoleTables;
using RoutesList.Build.Models;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public class Builder : IBuilder
    {
        readonly StringBuilder _stringBuilder;
        RoutesListOptions _options;
        IndexCompiler _indexCompiler;
        private string BodyContent { get; set; }
        public string Result { get; set; }
        public Builder()
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.index.html");

            if (stream == null) {
                throw new FileNotFoundException("something wrong with index.html");
            }

            _stringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
        }
        private void BuildHead()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            }

            _indexCompiler.CompileIndex(true);
        }

        private void BuildBody()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            } else {
                _indexCompiler.BodyContent = BodyContent;
            }

            _indexCompiler.CompileIndex(false, true);
        }

        private void BuildMeta()
        {
            throw new NotImplementedException();
        }

        private void BuildFooter()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            }

            _indexCompiler.CompileIndex(false, false, true);
        }

        private void BuildClass()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            }

            _indexCompiler.CompileIndex(false, false, false, true);
        }

#nullable enable
        public void Build(ConsoleTable? table, RoutesListOptions options)
        {
            if (table == null || options == null) {
                return;
            }

            _options = options;

            if (table != null) {
                var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.TablePartialView.html");

                if (stream == null) {
                    throw new FileNotFoundException("something wrong with TablePartialView.html");
                }

                var tableStringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());

                //Html structures factory
                HtmlData.GetInstance();

                HtmlData.Add<ConsoleTable>(table);

                BodyContent = HtmlStructureBodyCreator(new HtmlStructuresFactory(), tableStringBuilder);
            }

            BuildHead();
            BuildBody();
            BuildFooter();
            BuildClass();

            Result = _stringBuilder.ToString();
        }

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
