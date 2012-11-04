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
            var context = HostingRuntime.GetContext("Views");
            if (!context.IsStarted) {
                context.Start();
            }

            var references = viewContext.ControllerContext.Controller.ConfigureReferencedAssemblies();
            context.Host.ReferencedAssemblies.AddRange(references);

            var success = context.Host.RenderTemplate(viewContext.ViewName, _model, writer);
            if (!success) {
                throw new InvalidOperationException(context.Host.ErrorMessage);
            }

            viewContext.ControllerContext.ResponseWriter.Finish();
        }
    }
}
