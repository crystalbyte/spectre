using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.Threading;

namespace Crystalbyte.Spectre.UI {
    public sealed class BrowserRegistrar{
      private readonly List<Browser> _browsers;
        private static readonly BrowserRegistrar _current = new BrowserRegistrar();

        private BrowserRegistrar() {
            _browsers = new List<Browser>();
            if (Application.Current != null) {
                // This is necessary to allow the GC to collect all destroyed browser objects
                Application.Current.ShutdownStarted += (sender, e) => _browsers.Clear();
            }
        }

        public static BrowserRegistrar Current {
            get { return _current; }
        }

        public bool AnyRegistered() {
            return _browsers.Count > 0;
        }

        public void Register(Browser browser) {
            VerifyAccess();
            _browsers.Add(browser);
        }

        public bool IsBrowserAlive(Browser browser) {
            VerifyAccess();
            return _browsers.Any(x => x.Id == browser.Id);
        }

        public bool Remove(Browser context) {
            VerifyAccess();
            return _browsers.Remove(context);
        }

        public void VerifyAccess() {
            if (!Dispatcher.Current.IsCurrentlyOn(DispatcherQueue.Renderer)) {
                Errors.ThrowInvalidCrossThreadCall(DispatcherQueue.Renderer);
            }
        }
    }
}
