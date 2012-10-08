using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WindowsCefMainArgs {
		public IntPtr Instance;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct WindowsCefWindowInfo {
		public uint ExStyle;
		public CefStringUtf16 WindowName;
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
	
	
}
