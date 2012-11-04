using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public sealed class ViewResult : ActionResult {
        private readonly IView _view;

        public ViewResult(IView view) {
            _view = view;
        }

        public override void ExecuteResult(ControllerContext context) {
            _view.Render(new ViewContext(context), context.ResponseWriter.TextWriter);
        }
    }
}
