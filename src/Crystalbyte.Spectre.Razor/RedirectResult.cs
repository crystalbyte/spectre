using System;

namespace Crystalbyte.Spectre.Razor {
    public class RedirectResult : ActionResult {
        private readonly string _url;

        public RedirectResult(string url) {
            if (url == null) 
                throw new ArgumentNullException("url");
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context) {
            context.ResponseContext.RedirectUri = new Uri(_url);
        }
    }
}