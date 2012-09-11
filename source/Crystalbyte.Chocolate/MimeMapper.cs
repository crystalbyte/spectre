using System.Collections.Generic;

namespace Crystalbyte.Chocolate
{
    public static class MimeMapper {
        private static readonly Dictionary<string, string> _types = new Dictionary<string, string>();
        
        static MimeMapper() {
            _types.Add("cshtml", "text/html");
            _types.Add("vbhtml", "text/html");
            _types.Add("css", "text/css");
            _types.Add("js", "text/javascript");
            _types.Add("txt", "text/plain");
        }

        public static string ResolveFromExtension(string extension) {
            return _types.ContainsKey(extension) ? _types[extension] : "text/plain";
        }
    }
}
