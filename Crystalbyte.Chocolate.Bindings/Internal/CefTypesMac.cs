using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefMainArgs {
		int Argc;
		IntPtr Argv;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWindowInfo {
		CefStringUtf8 WindowName;
		int X;
		int Y;
		int Width;
		int Height;
		int Hidden;
		IntPtr ParentView;
		IntPtr View;
	}
	
	
	public enum CefGraphicsImplementation {
		DesktopInProcess = 0,
		DesktopInProcessCommandBuffer,
	}
	
}
