using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct LinuxCefMainArgs {
		public int Argc;
		public IntPtr Argv;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct LinuxCefWindowInfo {
		public IntPtr ParentWidget;
		public IntPtr Widget;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefTypesLinuxDelegates {
	}
	
}
