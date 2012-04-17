#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefProxyHandler {
        public CefBase Base;
        public IntPtr GetProxyForUrl;
    }

    public delegate void GetProxyForUrlCallback(IntPtr self, IntPtr url, IntPtr proxyInfo);
}