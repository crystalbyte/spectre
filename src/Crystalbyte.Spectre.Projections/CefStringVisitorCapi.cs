using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringVisitor {
		public CefBase Base;
		public IntPtr Visit;
	}
	
	
}
