#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class ClientHandler : OwnedAdapter {
        private readonly DisplayHandler _displayHandler;
        private readonly GetDisplayHandlerCallback _displayHandlerCallback;
        private readonly GetLifeSpanHandlerCallback _getLifeSpanHandlerCallback;
        private readonly LifeSpanHandler _lifeSpanHandler;

        public ClientHandler(BrowserDelegate @delegate)
            : base(typeof (CefClient)) {
            _displayHandler = new DisplayHandler(@delegate);
            _displayHandlerCallback = OnGetDisplayHandler;
            _lifeSpanHandler = new LifeSpanHandler(@delegate);
            _getLifeSpanHandlerCallback = OnLifeSpanHandlerCallback;

            MarshalToNative(new CefClient {
                Base = DedicatedBase,
                GetDisplayHandler = Marshal.GetFunctionPointerForDelegate(_displayHandlerCallback),
                GetLifeSpanHandler = Marshal.GetFunctionPointerForDelegate(_getLifeSpanHandlerCallback)
            });
        }

        private ClientHandler(IntPtr handle)
            : base(typeof (CefClient)) {
            NativeHandle = handle;
        }

        protected override void DisposeManaged() {
            base.DisposeManaged();
            _displayHandler.Dispose();
            _lifeSpanHandler.Dispose();
        }

        private IntPtr OnLifeSpanHandlerCallback(IntPtr self) {
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