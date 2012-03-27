using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ViewSettings : Adapter {
        public ViewSettings() 
        : base(typeof(CefBrowserSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings {
                Size = NativeSize
            });
        }
    }
}