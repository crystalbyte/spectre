#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class ClientHandler : OwnedAdapter {
        private readonly BrowserDelegate _delegate;
        private readonly LoadHandler _loadHandler;
        private readonly DisplayHandler _displayHandler;
        private readonly LifeSpanHandler _lifeSpanHandler;
        private readonly GeolocationHandler _geolocationHandler;
        private readonly GetLoadHandlerCallback _getLoadHandlerCallback;
        private readonly GetDisplayHandlerCallback _getDisplayHandlerCallback;
        private readonly GetLifeSpanHandlerCallback _getLifeSpanHandlerCallback;
        private readonly GetGeolocationHandlerCallback _getGeolocationHandlerCallback;
        private readonly OnProcessMessageRecievedCallback _processMessageReceivedCallback;

        public ClientHandler(BrowserDelegate @delegate)
            : base(typeof (CefClient)) {
            _delegate = @delegate;
            _displayHandler = new DisplayHandler(@delegate);
            _getDisplayHandlerCallback = OnGetDisplayHandler;
            _lifeSpanHandler = new LifeSpanHandler(@delegate);
            _getLifeSpanHandlerCallback = OnGetLifeSpanHandler;
            _loadHandler = new LoadHandler(@delegate);
            _getLoadHandlerCallback = OnGetLoadHandler;
            _geolocationHandler = new GeolocationHandler(@delegate);
            _getGeolocationHandlerCallback = OnGetGeolocationHandler;

            _processMessageReceivedCallback = OnProcessMessageReceived;

            MarshalToNative(new CefClient {
                Base = DedicatedBase,
                GetDisplayHandler = Marshal.GetFunctionPointerForDelegate(_getDisplayHandlerCallback),
                GetLifeSpanHandler = Marshal.GetFunctionPointerForDelegate(_getLifeSpanHandlerCallback),
                GetLoadHandler = Marshal.GetFunctionPointerForDelegate(_getLoadHandlerCallback),
                GetGeolocationHandler = Marshal.GetFunctionPointerForDelegate(_getGeolocationHandlerCallback),
                OnProcessMessageRecieved = Marshal.GetFunctionPointerForDelegate(_processMessageReceivedCallback)
            });
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

        private ClientHandler(IntPtr handle)
            : base(typeof (CefClient)) {
            NativeHandle = handle;
        }

        protected override void DisposeNative() {
            _displayHandler.Dispose();
            _lifeSpanHandler.Dispose();
            base.DisposeNative();
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

        internal static ClientHandler FromHandle(IntPtr handle) {
            return new ClientHandler(handle);
        }
    }
}