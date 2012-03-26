#region Namespace Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Chocolate {
    public static class Platform {
        static Platform() {
            // we will just assume the os does not change at runtime ...
            IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
            IsMacOS = !IsWindows && CheckForMacOS();
            IsLinux = !IsWindows && Environment.OSVersion.Platform == PlatformID.Unix && !IsMacOS;
        }

        // http://aautar.digital-radiation.com/blog/?p=1198

        public static bool IsWindows { get; private set; }

        public static bool IsLinux { get; private set; }

        public static bool IsMacOS { get; private set; }

        private static bool CheckForMacOS() {
            var buffer = IntPtr.Zero;
            try {
                buffer = Marshal.AllocHGlobal(8192);
                if (NativeMethods.UName(buffer) == 0) {
                    var os = Marshal.PtrToStringAnsi(buffer);
                    if (os == "Darwin") {
                        return true;
                    }
                }
                return false;
            }
            catch (DllNotFoundException ex) {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally {
                if (buffer != IntPtr.Zero) {
                    Marshal.FreeHGlobal(buffer);
                }
            }
        }

        #region Nested type: NativeMethods

        [SuppressUnmanagedCodeSecurity]
        private static class NativeMethods {
            [DllImport("libc", EntryPoint = "uname")]
            public static extern int UName(IntPtr buf);
        }

        #endregion
    }
}