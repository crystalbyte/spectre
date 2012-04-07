#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class BrowserSettings : Adapter {
        private readonly bool _isOwned;
        internal BrowserSettings()
            : base(typeof (CefBrowserSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefBrowserSettings {
                Size = NativeSize
            });
            _isOwned = true;
        }

        private BrowserSettings(IntPtr handle) 
            : base(typeof(CefBrowserSettings)) {
            NativeHandle = handle;
        }

        protected override void DisposeNative(){
            base.DisposeNative();
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
        }

        public static BrowserSettings FromHandle(IntPtr handle) {
            return new BrowserSettings(handle);
        }
    }
}