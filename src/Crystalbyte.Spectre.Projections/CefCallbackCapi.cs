using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCallback {
		public CefBase Base;
		public IntPtr Cont;
		public IntPtr Cancel;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefCallbackCapiDelegates {
		public delegate void ContCallback2(IntPtr self);
		public delegate void CancelCallback(IntPtr self);
	}
	
}
