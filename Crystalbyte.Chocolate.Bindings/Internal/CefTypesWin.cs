using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefMainArgs {
		public IntPtr Instance;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWindowInfo {
		public uint ExStyle;
		public CefStringUtf8 WindowName;
		public uint Style;
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public IntPtr ParentWindow;
		public IntPtr Menu;
		public bool TransparentPainting;
		public IntPtr Window;
	}
	
	
	public enum CefGraphicsImplementation {
		AngleInProcess = 0,
		AngleInProcessCommandBuffer,
		DesktopInProcess,
		DesktopInProcessCommandBuffer,
	}
	
}
