using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
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
