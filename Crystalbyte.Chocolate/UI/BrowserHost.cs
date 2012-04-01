#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class BrowserHost : Adapter {
        private BrowserHost(IntPtr handle)
            : base(typeof (CefBrowserHost), true) {
            NativeHandle = handle;
        }

        public ClientHandler Client {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetClientCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                                     typeof (GetClientCallback));
                var handle = function(NativeHandle);
                return ClientHandler.FromHandle(handle);
            }
        }

        public IntPtr OpenerWindowHandle {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetOpenerWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                                     typeof (GetOpenerWindowHandleCallback));
                return function(NativeHandle);
            }
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetWindowHandle,
                                                                     typeof (GetWindowHandleCallback));
                return function(NativeHandle);
            }
        }

        public Browser Browser {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser, typeof (GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public static Browser CreateBrowser(BrowserArgs a) {
            var uri = new StringUtf16(a.StartUri.OriginalString);
            Reference.Increment(a.ClientHandler.NativeHandle);
            var handle = CefBrowserCapi.CefBrowserHostCreateBrowserSync(
                a.WindowInfo.NativeHandle,
                a.ClientHandler.NativeHandle,
                uri.NativeHandle,
                a.Settings.NativeHandle);
            uri.Free();
            return Browser.FromHandle(handle);
        }

        public static BrowserHost FromHandle(IntPtr handle) {
            return new BrowserHost(handle);
        }

        public void ParentWindowWillClose() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (ParentWindowWillCloseCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.ParentWindowWillClose,
                                                               typeof (ParentWindowWillCloseCallback));
            action(NativeHandle);
        }

        public void Focus() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (SetFocusCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.SetFocus,
                                                               typeof (SetFocusCallback));
            action(NativeHandle, 1);
        }

        public void CloseBrowser() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (CloseBrowserCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                               typeof (CloseBrowserCallback));
            action(NativeHandle);
        }
    }
}