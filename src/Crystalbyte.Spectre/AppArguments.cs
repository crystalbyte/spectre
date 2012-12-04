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
