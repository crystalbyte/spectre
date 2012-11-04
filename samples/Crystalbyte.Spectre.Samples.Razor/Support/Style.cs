using System;

namespace Crystalbyte.Spectre.Samples.Support {
    public static class Style {
        public static string Link(string href) {
            return string.Format("<link href=\"{0}\" rel=\"stylesheet\">", href);
        }
    }
}
