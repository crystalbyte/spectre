#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class ProxyHandler : OwnedRefCountedNativeObject{
        private readonly AppDelegate _delegate;
        private readonly GetProxyForUrlCallback _getProxyForUrlCallback;

        public ProxyHandler(AppDelegate @delegate)
            : base(typeof (CefProxyHandler)){
            _delegate = @delegate;
            _getProxyForUrlCallback = GetProxyForUrl;
            MarshalToNative(new CefProxyHandler{
                                                   Base = DedicatedBase,
                                                   GetProxyForUrl =
                                                       Marshal.GetFunctionPointerForDelegate(_getProxyForUrlCallback)
                                               });
        }

        private void GetProxyForUrl(IntPtr self, IntPtr url, IntPtr proxyinfo){
            var e = new ProxyUrlEventArgs{
                                             Url = StringUtf16.ReadString(url)
                                         };
            _delegate.OnProxyForUrlRequested(e);
        }
    }
}