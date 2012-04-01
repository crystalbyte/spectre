#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class ClientHandler : CountedAdapter {
        private readonly RenderDelegate _delegate;

        public ClientHandler(RenderDelegate @delegate)
            : base(typeof (CefClient)) {
            _delegate = @delegate;
            MarshalToNative(new CefClient {
                Base = DedicatedBase
            });
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