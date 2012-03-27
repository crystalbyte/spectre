using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class Browser : Adapter {
        public Browser(BrowserArgs a) 
            : base(typeof(CefBrowser), true) {
            var uri = new StringUtf16(a.StartUri.AbsoluteUri);
            Reference.Increment(a.ClientHandler.NativeHandle);
            NativeHandle =
                CefBrowserCapi.CefBrowserCreateSync(
                    a.WindowInfo.NativeHandle,
                    a.ClientHandler.NativeHandle,
                    uri.NativeHandle,
                    a.Settings.NativeHandle);
        }

        public Browser(IntPtr handle)
            : base(typeof(CefBrowser), true) {
                NativeHandle = handle;
        }
    }
}
