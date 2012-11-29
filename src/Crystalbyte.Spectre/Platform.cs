#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Spectre {
    public static class Platform {
        static Platform() {
            // We will just assume the OS does not change at runtime, performing tests only once.
            IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
            IsOsX = !IsWindows && CheckForOsX();
            IsLinux = !IsWindows && Environment.OSVersion.Platform == PlatformID.Unix && !IsOsX;
        }

        public static bool IsWindows { get; private set; }

        public static bool IsLinux { get; private set; }

        public static bool IsOsX { get; private set; }

        /// <summary>
        ///   Determines whether current OS is OS X.
        ///   http://aautar.digital-radiation.com/blog/?p=1198
        /// </summary>
        /// <returns> True if current OS is OS X, else false. </returns>
        private static bool CheckForOsX() {
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
