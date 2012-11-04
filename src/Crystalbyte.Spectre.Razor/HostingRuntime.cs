using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Razor.Hosting.Containers;
using System.IO;

namespace Crystalbyte.Spectre.Razor {
    public static class HostingRuntime {
        private static readonly Dictionary<string, HostingContext> _containers;

        static HostingRuntime() {
            _containers = new Dictionary<string, HostingContext>();
        }

        public static HostingContext GetContext(string relativePath) {
            if (!_containers.ContainsKey(relativePath)) {
                _containers.Add(relativePath, new HostingContext(relativePath));
            }
            return _containers[relativePath];
        }
    }
}
