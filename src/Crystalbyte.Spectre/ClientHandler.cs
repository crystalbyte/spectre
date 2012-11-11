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
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class ClientHandler : OwnedRefCountedNativeObject {
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
            NativeHandle = handle;
        }

        private IntPtr OnGetContextMenuHandler(IntPtr self) {
            if (_contextMenuHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_contextMenuHandler.NativeHandle);
            return _contextMenuHandler.NativeHandle;
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
