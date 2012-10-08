using System;
using System.Runtime.InteropServices;

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
	
	
}
