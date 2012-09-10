using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public sealed class Resources
    {
        static  Resources() {
            Cache = new Dictionary<string, Stream>();
        }

        private static readonly IDictionary<string, Stream> Cache;

        public static Stream FindResource(string name) {
            if (!Cache.ContainsKey(name)) {
                var resource = typeof (Resources).Assembly.GetManifestResourceStream(name);
                Cache.Add(name, resource);
            }

            return Cache[name];
        }
    }
}
