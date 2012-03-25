using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefStringVisitor {
		CefBase Base;
		IntPtr Visit;
	}
	
	public delegate void VisitCallback(IntPtr self, IntPtr @string);
	
}
