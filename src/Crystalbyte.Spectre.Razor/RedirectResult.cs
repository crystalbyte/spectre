using System;

namespace Crystalbyte.Spectre.Razor {
    public class RedirectResult : ActionResult {
        private string _url;

        public RedirectResult(string url) {
            if (url == null) 
                throw new ArgumentNullException("url");
            _url = url;
        }
    }
}