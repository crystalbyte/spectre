using Crystalbyte.Spectre.Razor.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public abstract class Controller {
        public RequestContext RequestContext { get; private set; }
        public void Initialize(RequestContext context) {
            RequestContext = context;
        }

        public abstract ActionResult ComposeView();

        protected ViewResult View(object model) {
            return new ViewResult(new RazorView(model));
        }

        protected RedirectResult Redirect(string url) {
            return new RedirectResult(url);
        }
    }
}
