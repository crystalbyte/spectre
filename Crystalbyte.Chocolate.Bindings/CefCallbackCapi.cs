using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCallback {
		CefBase Base;
		IntPtr Cont;
		IntPtr Cancel;
	}
	
	public delegate void ContCallback(IntPtr self);
	public delegate void CancelCallback(IntPtr self);
	
}
