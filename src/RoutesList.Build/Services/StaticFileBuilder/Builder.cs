using ConsoleTables;
using RoutesList.Build.Models;
using RoutesList.Build.Services.StaticFileBuilder.HtmlStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public class Builder : IBuilder
    {
        StringBuilder _stringBuilder;
        Models.RoutesListOptions _options;
        IndexCompiler _indexCompiler;
        private string BodyContent { get; set; }
        public string Result { get; set; }
        public Builder()
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.index.html");

            if (stream == null) {
                throw new Exception("something wrong with index.html");
            }

            _stringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
        }
        private void BuildHead()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            } else {
                _indexCompiler.CompileIndex(true);
            }
        }

        private void BuildBody()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            } else {
                _indexCompiler.BodyContent = BodyContent;
                _indexCompiler.CompileIndex(true, true);
            }
        }

        private void BuildMeta()
        {
            throw new NotImplementedException();
        }

        private void BuildFooter()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
            } else {
                _indexCompiler.CompileIndex(true, true, true);
            }
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
                    throw new Exception("something wrong with TablePartialView.html");
                }

                var tableStringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());

                //TODO replace data in table string builder from table variable

                //Html structures factory
                HtmlStructureClient(new HtmlStructuresFactory(), tableStringBuilder);
            }

            BodyContent = table.ToString();

            BuildHead();
            BuildBody();
            BuildFooter();

            Result = _stringBuilder.ToString();
        }

        private StringBuilder HtmlStructureClient(
            IHtmlStructuresFactory factory,
            StringBuilder tableStringBuilder
        ) {
            var htmlTable = factory.CreateTableStructures(tableStringBuilder);

            //TODO return stringBuilder after build all structures
            StringBuilder sb = new StringBuilder(); // to be removed after the code has been implemented
            return sb;
        }
#nullable disable
    }
}
