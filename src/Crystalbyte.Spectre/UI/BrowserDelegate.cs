#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI {
    public class BrowserDelegate {

        public event EventHandler<ContextMenuOpeningEventArgs> ContextMenuOpening;

        protected internal virtual void OnContextMenuOpening(ContextMenuOpeningEventArgs e) {
            var handler = ContextMenuOpening;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ContextMenuCommandEventArgs> ContextMenuCommand;

        protected internal virtual void OnContextMenuCommand(ContextMenuCommandEventArgs e) {
            var handler = ContextMenuCommand;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<ContextMenuClosedEventArgs> ContextMenuClosed;

        protected internal void OnContextMenuClosed(ContextMenuClosedEventArgs e) {
            var handler = ContextMenuClosed;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<JavaScriptDialogOpeningEventArgs> JavaScriptDialogOpening;

        protected internal virtual void OnJavaScriptDialogOpening(JavaScriptDialogOpeningEventArgs e) {
            var handler = JavaScriptDialogOpening;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler<PageChangeNotificationDialogOpeningEventArgs> PageChangeNotificationDialogOpening;

        protected internal virtual void OnPageChangeNotificationDialogOpening(
            PageChangeNotificationDialogOpeningEventArgs e) {
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
