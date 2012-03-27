using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ApplicationSettings : Adapter {

        public ApplicationSettings() 
            : base(typeof(CefSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize
            });
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);    
            }
            base.DisposeNative();
        }
    }
}