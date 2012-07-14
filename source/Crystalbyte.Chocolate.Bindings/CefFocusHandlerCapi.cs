using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefFocusHandler {
		public CefBase Base;
		public IntPtr OnTakeFocus;
		public IntPtr OnSetFocus;
		public IntPtr OnGotFocus;
	}
	
	public delegate void OnTakeFocusCallback(IntPtr self, IntPtr browser, int next);
	public delegate int OnSetFocusCallback(IntPtr self, IntPtr browser, CefFocusSource source);
	public delegate void OnGotFocusCallback(IntPtr self, IntPtr browser);
	
}
