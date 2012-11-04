using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Samples.Support {
    public static class Html {
        public static string Image(string source, string alt) {
            return string.Format("<img src=\"{0}\" alt=\"{1}\" />", source, alt);
        }
        public static string Header(string text) {
            return string.Format("<h1>{0}</h1>", text);
        }
        public static string Span(string text) {
            return string.Format("<span>{0}</span>", text);
        }
    }
}
