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

        public event EventHandler<BrowserClosingEventArgs> Closing;

        protected internal virtual void OnClosing(BrowserClosingEventArgs e) {
            var handler = Closing;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserClosedEventArgs> Closed;

        protected internal virtual void OnClosed(BrowserClosedEventArgs e) {
            var handler = Closed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserLoadedEventArgs> Loaded;

        protected internal virtual void OnLoaded(BrowserLoadedEventArgs e) {
            var handler = Loaded;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<BrowserLoadingEventArgs> Loading;

        protected internal virtual void OnLoading(BrowserLoadingEventArgs e) {
            var handler = Loading;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<LoadingFailedEventArgs> LoadingFailed;

        protected internal virtual void OnLoadingFailed(LoadingFailedEventArgs e) {
            var handler = LoadingFailed;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}