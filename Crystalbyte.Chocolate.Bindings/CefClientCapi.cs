#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefClient {
        public CefBase Base;
        public IntPtr GetLifeSpanHandler;
        public IntPtr GetLoadHandler;
        public IntPtr GetRequestHandler;
        public IntPtr GetDisplayHandler;
        public IntPtr GetGeolocationHandler;
        public IntPtr OnProcessMessageRecieved;
    }

    public delegate IntPtr GetLifeSpanHandlerCallback(IntPtr self);

    public delegate IntPtr GetLoadHandlerCallback(IntPtr self);

    public delegate IntPtr GetRequestHandlerCallback(IntPtr self);

    public delegate IntPtr GetDisplayHandlerCallback(IntPtr self);

    public delegate IntPtr GetGeolocationHandlerCallback(IntPtr self);

    public delegate int OnProcessMessageRecievedCallback(
        IntPtr self, IntPtr browser, CefProcessId sourceProcess, IntPtr message);
}