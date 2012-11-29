using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

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
		[MarshalAs(UnmanagedType.U1)]
		public bool TransparentPainting;
		public IntPtr Window;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefTypesWinDelegates {
	}
	
}
