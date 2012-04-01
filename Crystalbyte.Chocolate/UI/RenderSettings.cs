#region Namespace Directives

using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class RenderSettings : Adapter {
        public RenderSettings()
            : base(typeof (CefBrowserSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings {
                Size = NativeSize
            });
        }
    }
}