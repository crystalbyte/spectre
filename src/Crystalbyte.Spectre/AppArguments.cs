#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre{
    internal static class AppArguments{
        public static IntPtr CreateForMac(string[] args){
            throw new NotImplementedException();
        }

        public static IntPtr CreateForLinux(string[] args){
            var mainArgs = new LinuxCefMainArgs{
                                                   Argc = args.Length,
                                                   Argv = StringArrayToPtr(args)
                                               };
            return MarshalArgs(mainArgs);
        }

        public static IntPtr CreateForWindows(IntPtr hInstance){
            var mainArgs = new WindowsCefMainArgs{
                                                     Instance = hInstance
                                                 };

            return MarshalArgs(mainArgs);
        }

        private static IntPtr MarshalArgs<T>(T mainArgs) where T : struct{
            var size = Marshal.SizeOf(typeof (T));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
        }

        public static IntPtr StringArrayToPtr(string[] strings){
            var ptrSize = Marshal.SizeOf(typeof (IntPtr));
            var destination = Marshal.AllocHGlobal(ptrSize*strings.Length);

            if (strings == null){
                throw new ArgumentNullException("strings");
            }

            for (var i = 0; i < strings.Length; ++i){
                var s = strings[i];
                var handle = Marshal.StringToHGlobalUni(s);
                Marshal.WriteIntPtr(handle, destination + (i*ptrSize));
            }

            return destination;
        }
    }
}