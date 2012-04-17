#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefResourceHandler {
        public CefBase Base;
        public IntPtr ProcessRequest;
        public IntPtr GetResponseHeaders;
        public IntPtr ReadResponse;
        public IntPtr Cancel;
    }

    public delegate int ProcessRequestCallback(IntPtr self, IntPtr request, IntPtr callback);

    public delegate void GetResponseHeadersCallback(
        IntPtr self, IntPtr response, IntPtr responseLength, IntPtr redirecturl);

    public delegate int ReadResponseCallback(
        IntPtr self, IntPtr dataOut, int bytesToRead, IntPtr bytesRead, IntPtr callback);
}