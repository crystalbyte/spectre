using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public sealed class AppSettings : Adapter {
        public readonly static AppSettings Default = new AppSettings();
        public AppSettings() 
            : base(typeof(CefSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize
            });
        }
    }
}