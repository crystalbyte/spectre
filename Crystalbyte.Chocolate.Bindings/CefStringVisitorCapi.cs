#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefStringVisitor {
        public CefBase Base;
        public IntPtr Visit;
    }

    public delegate void VisitCallback(IntPtr self, IntPtr @string);
}