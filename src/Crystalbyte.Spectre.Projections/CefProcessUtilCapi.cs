using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefProcessUtilCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_launch_process", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefLaunchProcess(IntPtr commandLine);
	}
	
}
