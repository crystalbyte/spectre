#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

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