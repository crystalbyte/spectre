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
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal static class AppArguments {
        public static IntPtr CreateForMac(string[] args) {
            throw new NotImplementedException();
        }

        public static IntPtr CreateForLinux(string[] args) {
            var mainArgs = new LinuxCefMainArgs {
                Argc = args.Length,
                Argv = StringArrayToPtr(args)
            };
            return MarshalArgs(mainArgs);
        }

        public static IntPtr CreateForWindows(IntPtr hInstance) {
            var mainArgs = new WindowsCefMainArgs {
                Instance = hInstance
            };

            return MarshalArgs(mainArgs);
        }

        private static IntPtr MarshalArgs<T>(T mainArgs) where T : struct {
            var size = Marshal.SizeOf(typeof (T));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
        }

        public static IntPtr StringArrayToPtr(string[] strings) {
            var ptrSize = Marshal.SizeOf(typeof (IntPtr));
            var destination = Marshal.AllocHGlobal(ptrSize*strings.Length);

            if (strings == null) {
                throw new ArgumentNullException("strings");
            }

            for (var i = 0; i < strings.Length; ++i) {
                var s = strings[i];
                var handle = Marshal.StringToHGlobalUni(s);
                Marshal.WriteIntPtr(handle, destination + (i*ptrSize));
            }

            return destination;
        }
    }
}