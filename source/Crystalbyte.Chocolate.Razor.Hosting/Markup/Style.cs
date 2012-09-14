using System;
using System.IO;
using Crystalbyte.Chocolate.Routing;

namespace Crystalbyte.Chocolate.Razor.Markup
{
    public static class Style
    {
        public static string Link(string relativePath) {
            return string.Format("<link rel=\"stylesheet\" href=\"{0}://siteoforigin:,,,{1}\"></link>", Schemes.Chocolate, relativePath);
        }
    }
}
