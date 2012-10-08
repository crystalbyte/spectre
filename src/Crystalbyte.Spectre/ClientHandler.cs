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
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class ClientHandler : RetainedNativeObject {
        private readonly BrowserDelegate _delegate;
        private readonly DisplayHandler _displayHandler;
        private readonly JavaScriptDialogHandler _javaScriptDialogHandler;
        private readonly LifeSpanHandler _lifeSpanHandler;
        private readonly LoadHandler _loadHandler;
        private readonly ContextMenuHandler _contextMenuHandler;
        
        private readonly GetJsdialogHandlerCallback _getJavaScriptdialogHandler;
        private readonly GeolocationHandler _geolocationHandler;
        private readonly GetDisplayHandlerCallback _getDisplayHandlerCallback;
        private readonly GetGeolocationHandlerCallback _getGeolocationHandlerCallback;
        private readonly GetLifeSpanHandlerCallback _getLifeSpanHandlerCallback;
        private readonly GetLoadHandlerCallback _getLoadHandlerCallback;
        private readonly GetContextMenuHandlerCallback _getContextMenuHandler;

        private readonly OnProcessMessageReceivedCallback _processMessageReceivedCallback;

        public ClientHandler(BrowserDelegate browserDelegate)
            : base(typeof (CefClient)) {
            _delegate = browserDelegate;
            _displayHandler = new DisplayHandler(browserDelegate);
            _getDisplayHandlerCallback = OnGetDisplayHandler;
            _lifeSpanHandler = new LifeSpanHandler(browserDelegate);
            _getLifeSpanHandlerCallback = OnGetLifeSpanHandler;
            _loadHandler = new LoadHandler(browserDelegate);
            _getLoadHandlerCallback = OnGetLoadHandler;
            _geolocationHandler = new GeolocationHandler(browserDelegate);
            _getGeolocationHandlerCallback = OnGetGeolocationHandler;
            _javaScriptDialogHandler = new JavaScriptDialogHandler(browserDelegate);
            _getJavaScriptdialogHandler = OnGetJavaScriptHandler;
            _contextMenuHandler = new ContextMenuHandler(browserDelegate);
            _getContextMenuHandler = OnGetContextMenuHandler;

            _processMessageReceivedCallback = OnProcessMessageReceived;

            MarshalToNative(new CefClient {
                Base = DedicatedBase,
                GetDisplayHandler = Marshal.GetFunctionPointerForDelegate(_getDisplayHandlerCallback),
                GetLifeSpanHandler = Marshal.GetFunctionPointerForDelegate(_getLifeSpanHandlerCallback),
                GetLoadHandler = Marshal.GetFunctionPointerForDelegate(_getLoadHandlerCallback),
                GetGeolocationHandler = Marshal.GetFunctionPointerForDelegate(_getGeolocationHandlerCallback),
                OnProcessMessageReceived = Marshal.GetFunctionPointerForDelegate(_processMessageReceivedCallback),
                GetJsdialogHandler = Marshal.GetFunctionPointerForDelegate(_getJavaScriptdialogHandler),
                GetContextMenuHandler = Marshal.GetFunctionPointerForDelegate(_getContextMenuHandler)
            });
        }

        private IntPtr OnGetContextMenuHandler(IntPtr self) {
            if (_contextMenuHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_contextMenuHandler.NativeHandle);
            return _contextMenuHandler.NativeHandle;
        }

        private ClientHandler(IntPtr handle)
            : base(typeof (CefClient)) {
            NativeHandle = handle;
        }

        private int OnProcessMessageReceived(IntPtr self, IntPtr browser, CefProcessId sourceprocess, IntPtr message) {
            var e = new IpcMessageReceivedEventArgs {
                Browser = Browser.FromHandle(browser),
                SourceProcess = (ProcessType) sourceprocess,
                Message = IpcMessage.FromHandle(message)
            };
            _delegate.OnIpcMessageReceived(e);
            return e.IsHandled ? 1 : 0;
        }

        private IntPtr OnGetJavaScriptHandler(IntPtr self) {
            if (_javaScriptDialogHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_javaScriptDialogHandler.NativeHandle);
            return _javaScriptDialogHandler.NativeHandle;
        }

        private IntPtr OnGetGeolocationHandler(IntPtr self) {
            if (_geolocationHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_geolocationHandler.NativeHandle);
            return _geolocationHandler.NativeHandle;
        }

        private IntPtr OnGetLoadHandler(IntPtr self) {
            if (_loadHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_loadHandler.NativeHandle);
            return _loadHandler.NativeHandle;
        }

        private IntPtr OnGetLifeSpanHandler(IntPtr self) {
            if (_lifeSpanHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_lifeSpanHandler.NativeHandle);
            return _lifeSpanHandler.NativeHandle;
        }

        private IntPtr OnGetDisplayHandler(IntPtr self) {
            if (_displayHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_displayHandler.NativeHandle);
            return _displayHandler.NativeHandle;
        }

        protected override void DisposeNative() {
            _displayHandler.Dispose();
            _lifeSpanHandler.Dispose();
            base.DisposeNative();
        }

        internal static ClientHandler FromHandle(IntPtr handle) {
            return new ClientHandler(handle);
        }
    }
}