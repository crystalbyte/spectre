using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ProxyHandler : OwnedAdapter {
        private readonly AppDelegate _delegate;
        private readonly GetProxyForUrlCallback _getProxyForUrlCallback;

        public ProxyHandler(AppDelegate @delegate)
            : base(typeof(CefProxyHandler)) {
            _delegate = @delegate;
            _getProxyForUrlCallback = GetProxyForUrl;
            MarshalToNative(new CefProxyHandler {
                Base = DedicatedBase,
                GetProxyForUrl = Marshal.GetFunctionPointerForDelegate(_getProxyForUrlCallback)
            });
        }

        private void GetProxyForUrl(IntPtr self, IntPtr url, IntPtr proxyinfo) {
            var e = new ProxyUrlEventArgs {
                Url = StringUtf16.ReadString(url)
            };
            _delegate.OnProxyForUrlRequested(e);
        }
    }
}