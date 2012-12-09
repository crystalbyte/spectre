#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
