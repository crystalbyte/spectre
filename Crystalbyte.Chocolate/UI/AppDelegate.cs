#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public class AppDelegate {
        public event EventHandler<BrowserEventArgs> BrowserDestroyed;

        protected internal virtual void OnBrowserDestroyed(BrowserEventArgs e) {
            var handler = BrowserDestroyed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<CommandLineEventArgs> CommandLineProcessing;

        protected internal virtual void OnCommandLineProcessing(CommandLineEventArgs e) {
            var handler = CommandLineProcessing;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ScriptingContextCreatedEventArgs> ScriptingContextCreated;

        protected internal virtual void OnContextCreated(ScriptingContextCreatedEventArgs e) {
            var handler = ScriptingContextCreated;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}