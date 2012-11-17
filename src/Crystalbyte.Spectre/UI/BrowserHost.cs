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
