using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre
{
	public static class Video
	{
		public static void Init ()
		{
			// this method call forces the CLR to load the libffmpegsumo.so file into memory.
			// This is necessary, since the default system wide "so" lib lookup does not search inside the program path.
			NativeMethods.Time();

		}

		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods {
			[DllImport("libffmpegsumo.so", EntryPoint = "time", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
			public static extern long Time();
		}
	}
}

