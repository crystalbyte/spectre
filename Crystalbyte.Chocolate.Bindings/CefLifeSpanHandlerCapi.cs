#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefLifeSpanHandler {
        public CefBase Base;
        public IntPtr OnBeforePopup;
        public IntPtr OnAfterCreated;
        public IntPtr RunModal;
        public IntPtr DoClose;
        public IntPtr OnBeforeClose;
    }

    public delegate int OnBeforePopupCallback(
        IntPtr self, IntPtr parentbrowser, IntPtr popupfeatures, IntPtr windowinfo, IntPtr url, IntPtr client,
        IntPtr settings);

    public delegate void OnAfterCreatedCallback(IntPtr self, IntPtr browser);

    public delegate int RunModalCallback(IntPtr self, IntPtr browser);

    public delegate int DoCloseCallback(IntPtr self, IntPtr browser);

    public delegate void OnBeforeCloseCallback(IntPtr self, IntPtr browser);
}