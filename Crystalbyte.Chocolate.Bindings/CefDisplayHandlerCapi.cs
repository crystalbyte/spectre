#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefDisplayHandler {
        public CefBase Base;
        public IntPtr OnLoadingStateChange;
        public IntPtr OnAddressChange;
        public IntPtr OnTitleChange;
        public IntPtr OnTooltip;
        public IntPtr OnConsoleMessage;
    }

    public delegate void OnLoadingStateChangeCallback(
        IntPtr self, IntPtr browser, int isloading, int cangoback, int cangoforward);

    public delegate void OnAddressChangeCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr url);

    public delegate void OnTitleChangeCallback(IntPtr self, IntPtr browser, IntPtr title);

    public delegate int OnTooltipCallback(IntPtr self, IntPtr browser, IntPtr text);

    public delegate int OnConsoleMessageCallback(IntPtr self, IntPtr browser, IntPtr message, IntPtr source, int line);
}