#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public class BrowserDelegate {

        public event EventHandler<JavaScriptDialogOpeningEventArgs> JavaScriptDialogOpening;

        protected internal virtual void OnJavaScriptDialogOpening(JavaScriptDialogOpeningEventArgs e) {
            var handler = JavaScriptDialogOpening;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<PageChangeNotificationDialogOpeningEventArgs> PageChangeNotificationDialogOpening;

        protected internal virtual void OnPageChangeNotificationDialogOpening(PageChangeNotificationDialogOpeningEventArgs e) {
            var handler = PageChangeNotificationDialogOpening;
            if (handler != null) {
                handler(this, e);
            }
        }

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

        public event EventHandler<PageLoadedEventArgs> PageLoaded;

        protected internal virtual void OnPageLoaded(PageLoadedEventArgs e) {
            var handler = PageLoaded;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<PageLoadingEventArgs> PageLoading;

        protected internal virtual void OnPageLoading(PageLoadingEventArgs e) {
            var handler = PageLoading;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<PageLoadingFailedEventArgs> PageLoadingFailed;

        protected internal virtual void OnPageLoadingFailed(PageLoadingFailedEventArgs e) {
            var handler = PageLoadingFailed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<GeolocationRequestedEventArgs> GeolocationRequested;

        protected internal virtual void OnGeolocationRequested(GeolocationRequestedEventArgs e) {
            var handler = GeolocationRequested;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<GeolocationRequestCanceledEventArgs> GeolocationRequestFailed;

        protected internal virtual void OnGeolocationRequestCanceled(GeolocationRequestCanceledEventArgs e) {
            var handler = GeolocationRequestFailed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<LoadingStateChangedEventArgs> LoadingStateChanged;

        protected internal virtual void OnLoadingStateChanged(LoadingStateChangedEventArgs e) {
            var handler = LoadingStateChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<IpcMessageReceivedEventArgs> IpcMessageReceived;

        protected internal virtual void OnIpcMessageReceived(IpcMessageReceivedEventArgs e) {
            var handler = IpcMessageReceived;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        protected internal virtual void OnTitleChanged(TitleChangedEventArgs e) {
            var handler = TitleChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<TooltipRequestedEventArgs> TooltipRequested;

        protected internal virtual void OnTooltipRequested(TooltipRequestedEventArgs e) {
            var handler = TooltipRequested;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<StatusMessageReceivedEventArgs> StatusMessageReceived;

        protected internal virtual void OnStatusMessageReceived(StatusMessageReceivedEventArgs e) {
            var handler = StatusMessageReceived;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}