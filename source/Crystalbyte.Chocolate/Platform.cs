#region Namespace Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Chocolate {
    public static class Platform {
        static Platform() {
            // We will just assume the OS does not change at runtime, performing tests only once.
            IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
            IsOSX = !IsWindows && CheckForOSX();
            IsLinux = !IsWindows && Environment.OSVersion.Platform == PlatformID.Unix && !IsOSX;
        }

        public static bool IsWindows { get; private set; }

        public static bool IsLinux { get; private set; }

        public static bool IsOSX { get; private set; }

        /// <summary>
        /// Determines whether current OS is OS X.
        /// http://aautar.digital-radiation.com/blog/?p=1198
        /// </summary>
        /// <returns>True if current OS is OS X, else false.</returns>
        private static bool CheckForOSX() {
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