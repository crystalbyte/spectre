#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class ClientHandler : OwnedAdapter {
        private readonly DisplayHandler _displayHandler;
        private readonly GetDisplayHandlerCallback _displayHandlerCallback;
        private readonly LifeSpanHandler _lifeSpanHandler;
        private readonly GetLifeSpanHandlerCallback _getLifeSpanHandlerCallback;

        public ClientHandler(ProcessDelegate @delegate)
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

        private ClientHandler(IntPtr handle)
            : base(typeof (CefClient)) {
            NativeHandle = handle;
        }

        internal static ClientHandler FromHandle(IntPtr handle) {
            return new ClientHandler(handle);
        }
    }
}