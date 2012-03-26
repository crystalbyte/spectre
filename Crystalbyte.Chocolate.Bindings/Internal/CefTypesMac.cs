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
		public CefStringUtf8 WindowName;
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public int Hidden;
		public IntPtr ParentView;
		public IntPtr View;
	}
	
	
	public enum CefGraphicsImplementation {
		DesktopInProcess = 0,
		DesktopInProcessCommandBuffer,
	}
	
}
