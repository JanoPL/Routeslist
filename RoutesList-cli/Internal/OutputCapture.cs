using System.Collections.Generic;

namespace RoutesList_cli.Internal
{
    public class OutputCapture
    {
        private readonly List<string> _lines = new List<string>();

        public IEnumerable<string> Lines => _lines;

        public void AddLine(string line) => _lines.Add(line);
    }
}
