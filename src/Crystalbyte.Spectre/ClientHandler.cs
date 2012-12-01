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
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class ClientHandler : OwnedRefCountedCefTypeAdapter {
        private readonly ContextMenuHandler _contextMenuHandler;
        private readonly BrowserDelegate _delegate;
        private readonly DisplayHandler _displayHandler;
        private readonly GeolocationHandler _geolocationHandler;
        private readonly CefClientCapiDelegates.GetContextMenuHandlerCallback _getContextMenuHandler;
        private readonly CefClientCapiDelegates.GetDisplayHandlerCallback _getDisplayHandlerCallback;
        private readonly CefClientCapiDelegates.GetGeolocationHandlerCallback _getGeolocationHandlerCallback;
        private readonly CefClientCapiDelegates.GetJsdialogHandlerCallback _getJavaScriptdialogHandler;
        private readonly CefClientCapiDelegates.GetLifeSpanHandlerCallback _getLifeSpanHandlerCallback;
        private readonly CefClientCapiDelegates.GetLoadHandlerCallback _getLoadHandlerCallback;
        private readonly JavaScriptDialogHandler _javaScriptDialogHandler;
        private readonly LifeSpanHandler _lifeSpanHandler;
        private readonly LoadHandler _loadHandler;

        private readonly CefClientCapiDelegates.OnProcessMessageReceivedCallback _processMessageReceivedCallback;

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
                GetDisplayHandler =
                    Marshal.GetFunctionPointerForDelegate(_getDisplayHandlerCallback),
                GetLifeSpanHandler =
                    Marshal.GetFunctionPointerForDelegate(_getLifeSpanHandlerCallback),
                GetLoadHandler =
                    Marshal.GetFunctionPointerForDelegate(_getLoadHandlerCallback),
                GetGeolocationHandler =
                    Marshal.GetFunctionPointerForDelegate(_getGeolocationHandlerCallback),
                OnProcessMessageReceived =
                    Marshal.GetFunctionPointerForDelegate(_processMessageReceivedCallback),
                GetJsdialogHandler =
                    Marshal.GetFunctionPointerForDelegate(_getJavaScriptdialogHandler),
                GetContextMenuHandler =
                    Marshal.GetFunctionPointerForDelegate(_getContextMenuHandler)
            });
        }

        private ClientHandler(IntPtr handle)
            : base(typeof (CefClient)) {
            Handle = handle;
        }

        private IntPtr OnGetContextMenuHandler(IntPtr self) {
            if (_contextMenuHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_contextMenuHandler.Handle);
            return _contextMenuHandler.Handle;
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
            Reference.Increment(_javaScriptDialogHandler.Handle);
            return _javaScriptDialogHandler.Handle;
        }

        private IntPtr OnGetGeolocationHandler(IntPtr self) {
            if (_geolocationHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_geolocationHandler.Handle);
            return _geolocationHandler.Handle;
        }

        private IntPtr OnGetLoadHandler(IntPtr self) {
            if (_loadHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_loadHandler.Handle);
            return _loadHandler.Handle;
        }

        private IntPtr OnGetLifeSpanHandler(IntPtr self) {
            if (_lifeSpanHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_lifeSpanHandler.Handle);
            return _lifeSpanHandler.Handle;
        }

        private IntPtr OnGetDisplayHandler(IntPtr self) {
            if (_displayHandler == null) {
                return IntPtr.Zero;
            }

            Reference.Increment(_displayHandler.Handle);
            return _displayHandler.Handle;
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
