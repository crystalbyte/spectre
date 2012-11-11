using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringVisitor {
		public CefBase Base;
		public IntPtr Visit;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefStringVisitorCapiDelegates {
		public delegate void VisitCallback3(IntPtr self, IntPtr @string);
	}
	
}
