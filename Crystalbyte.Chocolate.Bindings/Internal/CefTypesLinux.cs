#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.Bindings.Internal {
    [StructLayout(LayoutKind.Sequential)]
    public struct LinuxCefMainArgs {
        public int Argc;
        public IntPtr Argv;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LinuxCefWindowInfo {
        public IntPtr ParentWidget;
        public IntPtr Widget;
    }


    public enum LinuxCefGraphicsImplementation {
        DesktopInProcess = 0,
        DesktopInProcessCommandBuffer,
    }
}