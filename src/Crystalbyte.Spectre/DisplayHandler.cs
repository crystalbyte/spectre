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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class DisplayHandler : OwnedRefCountedNativeObject {
        private readonly CefDisplayHandlerCapiDelegates.OnAddressChangeCallback _addressChangeCallback;
        private readonly CefDisplayHandlerCapiDelegates.OnConsoleMessageCallback _consoleMessageCallback;
        private readonly BrowserDelegate _delegate;
        private readonly CefDisplayHandlerCapiDelegates.OnLoadingStateChangeCallback _loadingStateChangedCallback;
        private readonly CefDisplayHandlerCapiDelegates.OnStatusMessageCallback _statusMessageCallback;
        private readonly CefDisplayHandlerCapiDelegates.OnTitleChangeCallback _titleChangeCallback;
        private readonly CefDisplayHandlerCapiDelegates.OnTooltipCallback _tooltipCallback;

        public DisplayHandler(BrowserDelegate @delegate)
            : base(typeof (CefDisplayHandler)) {
            _delegate = @delegate;
            _tooltipCallback = OnTooltip;
            _titleChangeCallback = OnTitleChange;
            _statusMessageCallback = OnStatusMessage;
            _addressChangeCallback = OnAddressChange;
            _consoleMessageCallback = OnConsoleMessage;
            _loadingStateChangedCallback = OnLoadingStateChange;
            MarshalToNative(new CefDisplayHandler {
                Base = DedicatedBase,
                OnAddressChange =
                    Marshal.GetFunctionPointerForDelegate(_addressChangeCallback),
                OnConsoleMessage =
                    Marshal.GetFunctionPointerForDelegate(_consoleMessageCallback),
                OnLoadingStateChange =
                    Marshal.GetFunctionPointerForDelegate(
                        _loadingStateChangedCallback),
                OnTitleChange =
                    Marshal.GetFunctionPointerForDelegate(_titleChangeCallback),
                OnTooltip = Marshal.GetFunctionPointerForDelegate(_tooltipCallback),
                OnStatusMessage =
                    Marshal.GetFunctionPointerForDelegate(_statusMessageCallback)
            });
        }

        private void OnStatusMessage(IntPtr self, IntPtr browser, IntPtr value) {
            var e = new StatusMessageReceivedEventArgs {
                Browser = Browser.FromHandle(browser),
                Message = StringUtf16.ReadString(value),
            };
            _delegate.OnStatusMessageReceived(e);
        }

        private int OnTooltip(IntPtr self, IntPtr browser, IntPtr text) {
            var e = new TooltipRequestedEventArgs {
                Browser = Browser.FromHandle(browser),
                Text = StringUtf16.ReadString(text)
            };
            _delegate.OnTooltipRequested(e);
            return e.IsCanceled ? 1 : 0;
        }

        private void OnTitleChange(IntPtr self, IntPtr browser, IntPtr title) {
            var e = new TitleChangedEventArgs {
                Browser = Browser.FromHandle(browser),
                Title = StringUtf16.ReadString(title)
            };
            _delegate.OnTitleChanged(e);
        }

        private void OnLoadingStateChange(IntPtr self, IntPtr browser, int isloading, int cangoback, int cangoforward) {
            var e = new LoadingStateChangedEventArgs {
                Browser = Browser.FromHandle(browser),
                IsLoading = Convert.ToBoolean(isloading),
                CanGoBack = Convert.ToBoolean(cangoback),
                CanGoForward = Convert.ToBoolean(cangoforward)
            };
            _delegate.OnLoadingStateChanged(e);
        }

        private int OnConsoleMessage(IntPtr self, IntPtr browser, IntPtr message, IntPtr source, int line) {
            var e = new ConsoleMessageReceivedEventArgs {
                Browser = Browser.FromHandle(browser),
                Message = StringUtf16.ReadString(message),
                Source = StringUtf16.ReadString(source),
                Line = line
            };
            _delegate.OnConsoleMessageReceived(e);
            return e.IsSuppressed ? 1 : 0;
        }

        private void OnAddressChange(IntPtr self, IntPtr browser, IntPtr frame, IntPtr url) {
            var b = Browser.FromHandle(browser);
            var f = Frame.FromHandle(frame);
            var address = StringUtf16.ReadString(url);
            var e = new NavigatedEventArgs(address, b, f);
            _delegate.OnNavigated(e);
        }
    }
}
