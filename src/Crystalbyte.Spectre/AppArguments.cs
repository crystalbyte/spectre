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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre {
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
