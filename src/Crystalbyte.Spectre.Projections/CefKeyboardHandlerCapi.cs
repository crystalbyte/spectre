using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefKeyboardHandler {
		public CefBase Base;
		public IntPtr OnPreKeyEvent;
		public IntPtr OnKeyEvent;
	}
	
	public delegate int OnPreKeyEventCallback(IntPtr self, IntPtr browser, IntPtr @event, IntPtr osEvent, IntPtr isKeyboardShortcut);
	public delegate int OnKeyEventCallback(IntPtr self, IntPtr browser, IntPtr @event, IntPtr osEvent);
	
}
