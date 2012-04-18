#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI
{
    internal static class AppArguments
    {
		public static IntPtr CreateForMac(string[] args) {
			var mainArgs = new MacCefMainArgs {
				Argc = args.Length,
				Argv = IntPtr.Zero
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
			var size = Marshal.SizeOf(typeof(T));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
		}
    }
}
