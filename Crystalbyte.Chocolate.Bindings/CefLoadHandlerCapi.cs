#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefLoadHandler {
        public CefBase Base;
        public IntPtr OnLoadStart;
        public IntPtr OnLoadEnd;
    }

    public delegate void OnLoadStartCallback(IntPtr self, IntPtr browser, IntPtr frame);

    public delegate void OnLoadEndCallback(IntPtr self, IntPtr browser, IntPtr frame, int httpstatuscode);
}