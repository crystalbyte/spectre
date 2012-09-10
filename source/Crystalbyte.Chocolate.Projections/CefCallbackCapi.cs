using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCallback {
		public CefBase Base;
		public IntPtr Cont;
		public IntPtr Cancel;
	}
	
	public delegate void ContCallback(IntPtr self);
	public delegate void CancelCallback(IntPtr self);
	
}
