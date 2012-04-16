using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI
{
    public static class AppArguments
    {
        public static IntPtr Create(IntPtr appHandle) {
            if (Platform.IsWindows) {
                return CreateForWindows(appHandle);
            }

            throw new NotSupportedException("Runtime not supported.");
        }

        private static IntPtr CreateForWindows(IntPtr hInstance) {
            var mainArgs = new WindowsCefMainArgs {
                Instance = hInstance
            };
            var size = Marshal.SizeOf(typeof(WindowsCefMainArgs));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
        }
    }
}
