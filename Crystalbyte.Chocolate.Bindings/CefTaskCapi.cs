#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [SuppressUnmanagedCodeSecurity]
    public static class CefTaskCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_currently_on", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefCurrentlyOn(CefThreadId threadid);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_task", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefPostTask(CefThreadId threadid, IntPtr task);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_delayed_task", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefPostDelayedTask(CefThreadId threadid, IntPtr task, long delayMs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefTask {
        public CefBase Base;
        public IntPtr Execute;
    }

    public delegate void ExecuteCallback(IntPtr self, CefThreadId threadid);
}