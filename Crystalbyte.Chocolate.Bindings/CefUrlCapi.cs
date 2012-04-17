#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [SuppressUnmanagedCodeSecurity]
    public static class CefUrlCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_parse_url", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefParseUrl(IntPtr url, IntPtr parts);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_create_url", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefCreateUrl(IntPtr parts, IntPtr url);
    }
}