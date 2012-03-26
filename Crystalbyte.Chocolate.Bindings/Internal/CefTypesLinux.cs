using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefMainArgs {
		public int Argc;
		public IntPtr Argv;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWindowInfo {
		public IntPtr ParentWidget;
		public IntPtr Widget;
	}
	
	
	public enum CefGraphicsImplementation {
		DesktopInProcess = 0,
		DesktopInProcessCommandBuffer,
	}
	
}
