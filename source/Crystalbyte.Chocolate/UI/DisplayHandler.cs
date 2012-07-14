#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class DisplayHandler : OwnedAdapter {
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