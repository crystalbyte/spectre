using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefKeyboardHandler {
		public CefBase Base;
		public IntPtr OnPreKeyEvent;
		public IntPtr OnKeyEvent;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefKeyboardHandlerCapiDelegates {
		public delegate int OnPreKeyEventCallback(IntPtr self, IntPtr browser, IntPtr @event, IntPtr osEvent, ref int isKeyboardShortcut);
		public delegate int OnKeyEventCallback(IntPtr self, IntPtr browser, IntPtr @event, IntPtr osEvent);
	}
	
}
