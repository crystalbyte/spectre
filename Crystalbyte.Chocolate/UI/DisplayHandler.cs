#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class DisplayHandler : OwnedAdapter {
        private readonly OnAddressChangeCallback _addressChangeCallback;
        private readonly OnConsoleMessageCallback _consoleMessageCallback;
        private readonly BrowserDelegate _delegate;

        public DisplayHandler(BrowserDelegate @delegate)
            : base(typeof (CefDisplayHandler)) {
            _delegate = @delegate;
            _addressChangeCallback = OnAddressChange;
            _consoleMessageCallback = OnConsoleMessage;
            MarshalToNative(new CefDisplayHandler {
                Base = DedicatedBase,
                OnAddressChange = Marshal.GetFunctionPointerForDelegate(_addressChangeCallback),
                OnConsoleMessage = Marshal.GetFunctionPointerForDelegate(_consoleMessageCallback)
            });
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