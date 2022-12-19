using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// Template engine
    /// </summary>
    public class IndexCompiler
    {
        private readonly StringBuilder _stringBuilder;
        protected Dictionary<string, string> _header;
        protected Dictionary<string, string> _body;
        protected Dictionary<string, string> _footer;
        protected Dictionary<string, string> _classes;
        private readonly RoutesListOptions _options;
        public string BodyContent { get; set; } = string.Empty;
        public string AdditionalHeader { get; set; } = string.Empty;

        public IndexCompiler(StringBuilder stringBuilder, RoutesListOptions options)
        {
            _stringBuilder = stringBuilder;
            _options = options;
        }


        public StringBuilder CompileIndex(bool compileheader) 
        { 
            if (compileheader) {
                GetIndexHeader();
                ReplaceTag(_header);
            }

            return _stringBuilder;
        }

        public StringBuilder CompileIndex(bool compileHeader, bool compileBody)
        {
            if (compileHeader) {
                GetIndexHeader();
                ReplaceTag(_header);
            }

            if (compileBody) {
                GetIndexBody();
                ReplaceTag(_body);
            }

            return _stringBuilder;
        }

        public StringBuilder CompileIndex(bool compileHeader, bool compileBody, bool compileFooter)
        {
            if (compileHeader) {
                GetIndexHeader();
            }

            if (compileBody) {
                GetIndexBody();
                ReplaceTag(_body);
            }

            if (compileFooter) {
                GetIndexFooter();
                ReplaceTag(_footer);
            }

            return _stringBuilder;
        }
        public StringBuilder CompileIndex(
            bool compileHeader,
            bool compileBody,
            bool compileFooter,
            bool compileClasses
        ) {
            if (compileHeader) {
                GetIndexHeader();
                ReplaceTag(_header);
            }

            if (compileBody) {
                GetIndexBody();
                ReplaceTag(_body);
            }

            if (compileFooter) {
                GetIndexFooter();
                ReplaceTag(_footer);
            }

            if (compileClasses) {
                GetIndexClass();
                ReplaceTag(_classes);
            }
            
            return _stringBuilder;
        }

        private void ReplaceTag(Dictionary<string, string> data)
        {
            foreach (var item in data) {
                _stringBuilder.Replace(item.Key, item.Value);
            }
        }

        private void GetIndexBody()
        {
            _body = new Dictionary<string, string>() {
                { "$(body)", BodyContent }
            };
        }

        private void GetIndexHeader()
        {
            _header = new Dictionary<string, string>() {
                { "$(charsetEncoding)", _options.CharSet },
                { "$(title)", _options.Tittle },
                { "$(additionalHead)",  AdditionalHeader },
                { "$(description)", _options.Description },
            };
        }

        private void GetIndexFooter()
        {
            _footer = new Dictionary<string, string>() {
                { "$(footer-link)", _options.FooterLink },
                { "$(footer-text)", _options.FooterText },
                { "$(footer-year)", DateTime.Now.Year.ToString() }
            };
        }

        private void GetIndexClass()
        {
            string classes = "table";

            if (_options.GetClasses() != null && _options.GetClasses().Length > 0) {
                classes = String.Join(" ", _options.GetClasses());
            }

            _classes = new Dictionary<string, string>() {
                { "$(table-classes)", classes },
            };
        }
    }
}
