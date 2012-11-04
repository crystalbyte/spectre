using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Razor.Hosting;
using Crystalbyte.Spectre.Razor.Hosting.Containers;
using Crystalbyte.Spectre.Razor.Templates;

namespace Crystalbyte.Spectre.Razor {
    public class RazorView : IView {
        private readonly object _model;

        public RazorView(object model) {
            _model = model;
        }

        public void Render(ViewContext viewContext, TextWriter writer) {
            var name = viewContext.ViewName;

            var context = HostingRuntime.GetContext("Controllers");
            if (!context.IsStarted) {
                context.Start();
            }

            
            



        }
    }
}
