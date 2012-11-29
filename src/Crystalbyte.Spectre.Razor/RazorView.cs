#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.IO;

#endregion

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
