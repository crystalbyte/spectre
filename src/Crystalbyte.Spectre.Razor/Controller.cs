using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.Web;

namespace Crystalbyte.Spectre.Razor {
    public abstract class Controller {
        protected Controller() {
            _list = new List<string> {
                "Crystalbyte.Spectre.Razor.dll"
            };
        }

        public void Initialize(Request request) {
            if (request == null) 
                throw new ArgumentNullException("request");

            Request = request;
        }

        protected Request Request { get; private set; }

        public abstract ActionResult Execute();

        protected ViewResult View(object model) {
            return new ViewResult(new RazorView(model));
        }

        protected RedirectResult Redirect(string url) {
            return new RedirectResult(url);
        }

        private readonly IList<string> _list;
        public virtual IList<string> ConfigureReferencedAssemblies() {
            return _list;
        }
    }
}
