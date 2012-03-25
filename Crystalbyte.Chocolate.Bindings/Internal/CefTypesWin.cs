using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefMainArgs {
		IntPtr Instance;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefWindowInfo {
		int ExStyle;
		CefStringUtf8 WindowName;
		int Style;
		int X;
		int Y;
		int Width;
		int Height;
		IntPtr ParentWindow;
		IntPtr Menu;
		bool TransparentPainting;
		IntPtr Window;
	}
	
	
	public enum CefGraphicsImplementation {
		AngleInProcess = 0,
		AngleInProcessCommandBuffer,
		DesktopInProcess,
		DesktopInProcessCommandBuffer,
	}
	
}
