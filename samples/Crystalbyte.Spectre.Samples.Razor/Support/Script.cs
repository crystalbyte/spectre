using System;

namespace Crystalbyte.Spectre.Samples.Support {
    public static class Script {
        public static string Reference(string href) {
            return string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", href);
        }
    }
}
