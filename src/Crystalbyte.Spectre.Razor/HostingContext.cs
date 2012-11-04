using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Razor.Hosting.Containers;

namespace Crystalbyte.Spectre.Razor {
    public class HostingContext {
        private readonly RazorFolderHostContainer _host;

        public HostingContext(string relativePath) {
            _host = new RazorFolderHostContainer {
                TemplatePath = relativePath
            };
        }

        public List<string> ReferencedAssemblies {
            get { return _host.ReferencedAssemblies; }
        }

        public bool IsStarted { get; set; }

        public void Start() {
            _host.Start();
            IsStarted = true;
        }

        public RazorFolderHostContainer Host {
            get {
                return _host;
            }
        }
    }
}
