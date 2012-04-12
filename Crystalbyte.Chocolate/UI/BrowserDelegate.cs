#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public class BrowserDelegate {
        public event EventHandler<NavigatedEventArgs> Navigated;

        protected internal virtual void OnNavigated(NavigatedEventArgs e) {
            var handler = Navigated;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ConsoleMessageReceivedEventArgs> ConsoleMessageReceived;

        protected internal virtual void OnConsoleMessageReceived(ConsoleMessageReceivedEventArgs e) {
            var handler = ConsoleMessageReceived;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<PopupCreatingEventArgs> PopupCreating;

        protected internal virtual void OnPopupCreating(PopupCreatingEventArgs e) {
            var handler = PopupCreating;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserClosingEventArgs> BrowserClosing;

        protected internal virtual void OnBrowserClosing(BrowserClosingEventArgs e) {
            var handler = BrowserClosing;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserClosedEventArgs> BrowserClosed;

        protected internal virtual void OnBrowserClosed(BrowserClosedEventArgs e) {
            var handler = BrowserClosed;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}