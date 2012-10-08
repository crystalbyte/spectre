using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections.Internal
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
