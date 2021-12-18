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
        public Builder()
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream("RoutesList.Build.Resources.StaticFile.index.html");

            if (stream == null) {
                throw new Exception("something wrong with index.html");
            }

            _stringBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
        }
        public void BuildHead()
        {
            if (_indexCompiler == null) {
                _indexCompiler = new IndexCompiler(_stringBuilder, _options);
               
            } else {
                _indexCompiler.CompileIndex(true);
            }
        }

        public void BuildBody()
        {
            throw new NotImplementedException();
        }


        public void BuildMeta()
        {
            throw new NotImplementedException();
        }
    }
}
