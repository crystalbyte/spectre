#region Namespace Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class Browser : Adapter {
        private Browser(IntPtr handle)
            : base(typeof (CefBrowser), true) {
            NativeHandle = handle;
            Debug.WriteLine(string.Format("Browser: {0}", NativeHandle));
        }

        internal BrowserHost Host {
            get {
                var reflection = MarshalFromNative<CefBrowser>();
                var function = (GetHostCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetHost, typeof (GetHostCallback));
                var handle = function(NativeHandle);
                return BrowserHost.FromHandle(handle);
            }
        }

        public static Browser FromHandle(IntPtr handle) {
            return new Browser(handle);
        }

        public void Navigate(string url) {
            
        }
    }
}