using System;
using System.IO;

namespace Crystalbyte.Chocolate.Razor.Markup
{
    public static class Style
    {
        public static string Link(string relativePath) {
            return string.Format("<link rel=\"stylesheet\" href=\"mvc://siteoforigin:,,,/{0}\"></link>", relativePath);
        }
    }
}
