using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.IO;

namespace Crystalbyte.Chocolate.Razor.Markup
{
    public static class Script
    {
        public static string Reference(string url) {
            var uri = url.StartsWith("/") 
                ? new Uri(string.Format("{0}://siteoforigin:,,,{1}", Schemes.Pack, url)) 
                : new Uri(url);

            return string.Format("<script src=\"{0}\" type=\"{1}\"></script>", uri.AbsoluteUri, "text/javascript");
        }
    }
}
