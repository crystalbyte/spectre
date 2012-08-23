using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
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
