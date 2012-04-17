#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefCallback {
        public CefBase Base;
        public IntPtr Cont;
        public IntPtr Cancel;
    }

    public delegate void ContCallback(IntPtr self);

    public delegate void CancelCallback(IntPtr self);
}