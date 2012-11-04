using System;
using Crystalbyte.Spectre.Web;

namespace Crystalbyte.Spectre.Razor {
    public abstract class Controller {
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
    }
}
