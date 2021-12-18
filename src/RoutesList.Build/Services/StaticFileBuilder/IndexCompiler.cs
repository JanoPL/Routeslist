using System;
using System.Collections.Generic;
using System.Text;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// Template engine
    /// </summary>
    public class IndexCompiler
    {
        private StringBuilder _stringBuilder;
        protected Dictionary<string, string> _header;
        protected Dictionary<string, string> _body;
        protected Dictionary<string, string> _footer;
        //protected Dictionary<string, Func> _conditions;
        private readonly RoutesListOptions _options;

        public IndexCompiler(StringBuilder stringBuilder, RoutesListOptions options)
        {
            _stringBuilder = stringBuilder;
            _options = options;
        }

        public StringBuilder CompileIndex(
            bool compileHeader,
            bool compileBody = false,
            bool compileFooter = false,
            bool compileCondition = false
        ) {
            if (compileHeader) {
                _header = GetIndexHeader();
                //TODO replace in index words
            }

            if (compileBody) {
                _body = GetIndexBody();
                //TODO replace in index words
            }

            if (compileHeader && compileBody && compileFooter) {
                _footer = GetIndexFooter();
                //TODO replace in index words 
            }

            //if (compileCondition) {
            //    _conditions = GetIndexCondition();
            //}
            
            return _stringBuilder;
        }

        private Dictionary<string, string> GetIndexBody()
        {
            return new Dictionary<string, string>() {
                { "$(body)", "" }
            };
        }

        private Dictionary<string, string> GetIndexHeader()
        {
            return new Dictionary<string, string>() {
                { "$(charsetEncoding)", _options.CharSet },
                { "$(title)", _options.Tittle }
            };
        }
        private Dictionary<string, string> GetIndexFooter()
        {
            return new Dictionary<string, string>() {
                { "$(footer)", "" }
            };
        }
    }
}
