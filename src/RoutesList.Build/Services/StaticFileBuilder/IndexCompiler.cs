using System;
using System.Collections.Generic;
using System.Text;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    /// <summary>
    /// Compiles and processes HTML template files by replacing placeholder tags with actual content.
    /// </summary>
    public class IndexCompiler
    {
        private readonly StringBuilder _stringBuilder;
        private Dictionary<string, string> _header;
        private Dictionary<string, string> _body;
        private Dictionary<string, string> _footer;
        private Dictionary<string, string> _classes;
        private readonly RoutesListOptions _options;
        /// <summary>
        /// Gets or sets the content to be inserted into the body section of the template.
        /// </summary>
        public string BodyContent { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets additional content to be inserted into the header section of the template.
        /// </summary>
        private string AdditionalHeader { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCompiler"/> class.
        /// </summary>
        /// <param name="stringBuilder">The StringBuilder instance used for template processing.</param>
        /// <param name="options">The options containing configuration for template compilation.</param>
        public IndexCompiler(StringBuilder stringBuilder, RoutesListOptions options)
        {
            _stringBuilder = stringBuilder;
            _options = options;
        }


        /// <summary>
        /// Compiles the template with optional header compilation.
        /// </summary>
        /// <param name="compileHeaderFlag">If true, includes header compilation in the process.</param>
        /// <returns>The processed StringBuilder instance containing the compiled template.</returns>
        public StringBuilder CompileIndex(bool compileHeaderFlag)
        {
            if (compileHeaderFlag) {
                GetIndexHeader();
                ReplaceTag(_header);
            }

            return _stringBuilder;
        }

        /// <summary>
        /// Compiles the template with optional header and body compilation.
        /// </summary>
        /// <param name="compileHeader">If true, includes header compilation in the process.</param>
        /// <param name="compileBody">If true, includes body compilation in the process.</param>
        /// <returns>The processed StringBuilder instance containing the compiled template.</returns>
        public StringBuilder CompileIndex(bool compileHeader, bool compileBody)
        {
            if (compileHeader) {
                GetIndexHeader();
                ReplaceTag(_header);
            }

            if (!compileBody)
            {
                return _stringBuilder;
            }
            
            GetIndexBody();
            ReplaceTag(_body);

            return _stringBuilder;
        }

        /// <summary>
        /// Compiles the template with optional header, body, and footer compilation.
        /// </summary>
        /// <param name="compileHeader">If true, includes header compilation in the process.</param>
        /// <param name="compileBody">If true, includes body compilation in the process.</param>
        /// <param name="compileFooter">If true, includes footer compilation in the process.</param>
        /// <returns>The processed StringBuilder instance containing the compiled template.</returns>
        public StringBuilder CompileIndex(bool compileHeader, bool compileBody, bool compileFooter)
        {
            if (compileHeader) {
                GetIndexHeader();
            }

            if (compileBody) {
                GetIndexBody();
                ReplaceTag(_body);
            }

            if (!compileFooter) return _stringBuilder;
            
            GetIndexFooter();
            ReplaceTag(_footer);

            return _stringBuilder;
        }

        /// <summary>
        /// Compiles the template with optional header, body, footer, and CSS classes compilation.
        /// </summary>
        /// <param name="compileHeader">If true, includes header compilation in the process.</param>
        /// <param name="compileBody">If true, includes body compilation in the process.</param>
        /// <param name="compileFooter">If true, includes footer compilation in the process.</param>
        /// <param name="compileClasses">If true, includes CSS classes compilation in the process.</param>
        /// <returns>The processed StringBuilder instance containing the compiled template.</returns>
        public StringBuilder CompileIndex(
            bool compileHeader,
            bool compileBody,
            bool compileFooter,
            bool compileClasses
        )
        {
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

            if (!compileClasses)
            {
                return _stringBuilder;
            }
            
            GetIndexClass();
            ReplaceTag(_classes);

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
            _body = new Dictionary<string, string> {
                { "$(body)", BodyContent }
            };
        }

        private void GetIndexHeader()
        {
            _header = new Dictionary<string, string> {
                { "$(charsetEncoding)", _options.CharSet },
                { "$(title)", _options.Title },
                { "$(additionalHead)",  AdditionalHeader },
                { "$(description)", _options.Description },
            };
        }

        private void GetIndexFooter()
        {
            _footer = new Dictionary<string, string> {
                { "$(footer-link)", _options.FooterLink },
                { "$(footer-text)", _options.FooterText },
                { "$(footer-year)", DateTime.Now.Year.ToString() }
            };
        }

        private void GetIndexClass()
        {
            string classes = _options.Classes switch
            {
                string[] classArray when classArray.Length > 0 => String.Join(" ", classArray),
                string classString when !string.IsNullOrEmpty(classString) => classString,
                _ => "table"
            };

            _classes = new Dictionary<string, string> {
                { "$(table-classes)", classes },
            };
        }
    }
}