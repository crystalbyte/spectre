using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefProcessUtilCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_launch_process", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefLaunchProcess(IntPtr commandLine);
	}
	
}
