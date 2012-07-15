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
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class BrowserHost : Adapter {
        private BrowserHost(IntPtr handle)
            : base(typeof (CefBrowserHost), true) {
            NativeHandle = handle;
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

        public static Browser CreateBrowser(BrowserCreationArgs a) {
            var uri = new StringUtf16(a.StartUri.AbsoluteUri);
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