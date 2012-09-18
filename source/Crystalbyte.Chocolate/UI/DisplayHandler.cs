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
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.Projections.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class DisplayHandler : RefCountedNativeObject {
        private readonly OnAddressChangeCallback _addressChangeCallback;
        private readonly OnConsoleMessageCallback _consoleMessageCallback;
        private readonly BrowserDelegate _delegate;
        private readonly OnLoadingStateChangeCallback _loadingStateChangedCallback;
        private readonly OnStatusMessageCallback _statusMessageCallback;
        private readonly OnTitleChangeCallback _titleChangeCallback;
        private readonly OnTooltipCallback _tooltipCallback;

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
                OnAddressChange = Marshal.GetFunctionPointerForDelegate(_addressChangeCallback),
                OnConsoleMessage = Marshal.GetFunctionPointerForDelegate(_consoleMessageCallback),
                OnLoadingStateChange = Marshal.GetFunctionPointerForDelegate(_loadingStateChangedCallback),
                OnTitleChange = Marshal.GetFunctionPointerForDelegate(_titleChangeCallback),
                OnTooltip = Marshal.GetFunctionPointerForDelegate(_tooltipCallback),
                OnStatusMessage = Marshal.GetFunctionPointerForDelegate(_statusMessageCallback)
            });
        }

        private void OnStatusMessage(IntPtr self, IntPtr browser, IntPtr value, CefHandlerStatustype type) {
            var e = new StatusMessageReceivedEventArgs {
                Browser = Browser.FromHandle(browser),
                Message = StringUtf16.ReadString(value),
                StatusType = (StatusType) type
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