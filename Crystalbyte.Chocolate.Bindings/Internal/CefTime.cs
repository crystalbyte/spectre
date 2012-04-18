#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Chocolate.Bindings.Internal {
    [SuppressUnmanagedCodeSecurity]
    public static class CefTimeClass {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_timet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeToTimet(IntPtr cefTime, IntPtr time);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_timet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeFromTimet(long time, IntPtr cefTime);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_doublet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeToDoublet(IntPtr cefTime, IntPtr time);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_doublet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeFromDoublet(Double time, IntPtr cefTime);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefTime {
        public int Year;
        public int Month;
        public int DayOfWeek;
        public int DayOfMonth;
        public int Hour;
        public int Minute;
        public int Second;
        public int Millisecond;
    }
}