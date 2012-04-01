#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefAuthCallback {
        public CefBase Base;
        public IntPtr Cont;
        public IntPtr Cancel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefRequestHandler {
        public CefBase Base;
        public IntPtr OnBeforeResourceLoad;
        public IntPtr GetResourceHandler;
        public IntPtr OnResourceRedirect;
        public IntPtr GetAuthCredentials;
    }

    public delegate int OnBeforeResourceLoadCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);

    public delegate IntPtr GetResourceHandlerCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);

    public delegate void OnResourceRedirectCallback(
        IntPtr self, IntPtr browser, IntPtr frame, IntPtr oldUrl, IntPtr newUrl);

    public delegate int GetAuthCredentialsCallback(
        IntPtr self, IntPtr browser, IntPtr frame, int isproxy, IntPtr host, int port, IntPtr realm, IntPtr scheme,
        IntPtr callback);
}