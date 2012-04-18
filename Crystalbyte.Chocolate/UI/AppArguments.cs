using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI
{
    internal static class AppArguments
    {
		public static IntPtr CreateForMac(string[] args) {
			var mainArgs = new MacCefMainArgs {
				Argc = args.Length,
				Argv = IntPtr.Zero
			};
			return Marshal<MacCefMainArgs>(mainArgs);
		}

        public static IntPtr CreateForWindows(IntPtr hInstance) {
            var mainArgs = new WindowsCefMainArgs {
                Instance = hInstance
            };
            return Marshal<WindowsCefMainArgs>(mainArgs);
        }
		
		private static IntPtr Marshal<T>(T mainArgs) where T : struct {
			var size = Marshal.SizeOf(typeof(T));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
		}
    }
}
