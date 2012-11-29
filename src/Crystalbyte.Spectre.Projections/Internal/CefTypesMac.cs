using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct MacCefMainArgs {
		public int Argc;
		public IntPtr Argv;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct MacCefWindowInfo {
		public CefStringUtf16 WindowName;
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public int Hidden;
		public IntPtr ParentView;
		public IntPtr View;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefTypesMacDelegates {
	}
	
}
