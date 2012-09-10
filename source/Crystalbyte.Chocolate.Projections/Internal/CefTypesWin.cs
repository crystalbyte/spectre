using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections.Internal
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
