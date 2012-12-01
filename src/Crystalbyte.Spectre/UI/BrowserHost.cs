#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI {
    internal sealed class BrowserHost : RefCountedCefTypeAdapter {
        private BrowserHost(IntPtr handle)
            : base(typeof (CefBrowserHost)) {
            Handle = handle;
        }

        public IntPtr OpenerWindowHandle {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (CefBrowserCapiDelegates.GetOpenerWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.
                                                                         GetOpenerWindowHandleCallback));
                return function(Handle);
            }
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (CefBrowserCapiDelegates.GetWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetWindowHandle,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetWindowHandleCallback
                                                                         ));
                return function(Handle);
            }
        }

        public Browser Browser {
            get {
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (CefBrowserCapiDelegates.GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser,
                                                                     typeof (CefBrowserCapiDelegates.GetBrowserCallback));
                var handle = function(Handle);
                return Browser.FromHandle(handle);
            }
        }

        public static Browser CreateBrowser(BrowserCreationArgs a) {
            var uri = new StringUtf16(a.StartUri.AbsoluteUri);
            Reference.Increment(a.ClientHandler.Handle);
            var handle = CefBrowserCapi.CefBrowserHostCreateBrowserSync(
                a.WindowInfo.Handle,
                a.ClientHandler.Handle,
                uri.Handle,
                a.Settings.Handle);
            uri.Free();
            return Browser.FromHandle(handle);
        }

        public static BrowserHost FromHandle(IntPtr handle) {
            return new BrowserHost(handle);
        }

        public void ParentWindowWillClose() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (CefBrowserCapiDelegates.ParentWindowWillCloseCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.ParentWindowWillClose,
                                                               typeof (
                                                                   CefBrowserCapiDelegates.ParentWindowWillCloseCallback
                                                                   ));
            action(Handle);
        }

        public void Focus() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (CefBrowserCapiDelegates.SetFocusCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.SetFocus,
                                                               typeof (CefBrowserCapiDelegates.SetFocusCallback));
            action(Handle, 1);
        }

        public void CloseBrowser() {
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (CefBrowserCapiDelegates.CloseBrowserCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                               typeof (CefBrowserCapiDelegates.CloseBrowserCallback));
            action(Handle);
        }
    }
}
