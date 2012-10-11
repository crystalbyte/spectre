﻿#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI{
    internal sealed class BrowserHost : RefCountedNativeObject{
        private BrowserHost(IntPtr handle)
            : base(typeof (CefBrowserHost)){
            NativeHandle = handle;
        }

        public IntPtr OpenerWindowHandle{
            get{
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetOpenerWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                                     typeof (GetOpenerWindowHandleCallback));
                return function(NativeHandle);
            }
        }

        public IntPtr WindowHandle{
            get{
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetWindowHandleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetWindowHandle,
                                                                     typeof (GetWindowHandleCallback));
                return function(NativeHandle);
            }
        }

        public Browser Browser{
            get{
                var reflection = MarshalFromNative<CefBrowserHost>();
                var function = (GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser, typeof (GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public static Browser CreateBrowser(BrowserCreationArgs a){
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

        public static BrowserHost FromHandle(IntPtr handle){
            return new BrowserHost(handle);
        }

        public void ParentWindowWillClose(){
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (ParentWindowWillCloseCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.ParentWindowWillClose,
                                                               typeof (ParentWindowWillCloseCallback));
            action(NativeHandle);
        }

        public void Focus(){
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (SetFocusCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.SetFocus,
                                                               typeof (SetFocusCallback));
            action(NativeHandle, 1);
        }

        public void CloseBrowser(){
            var reflection = MarshalFromNative<CefBrowserHost>();
            var action = (CloseBrowserCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.GetOpenerWindowHandle,
                                                               typeof (CloseBrowserCallback));
            action(NativeHandle);
        }
    }
}