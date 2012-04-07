using System;

namespace Crystalbyte.Chocolate.UI {
    public class ProcessDelegate {
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

        public event EventHandler<BrowserEventArgs> BrowserDestroyed;
        protected internal virtual void OnBrowserDestroyed(BrowserEventArgs e) {
            var handler = BrowserDestroyed;
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

        public event EventHandler<BrowserClosedEventArgs> PopupClosed;
        protected internal virtual void OnBrowserClosed(BrowserClosedEventArgs e) {
            var handler = PopupClosed;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}