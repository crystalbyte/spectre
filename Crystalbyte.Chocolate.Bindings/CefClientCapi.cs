using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefClient {
		CefBase Base;
		IntPtr GetLifeSpanHandler;
		IntPtr GetLoadHandler;
		IntPtr GetRequestHandler;
		IntPtr GetDisplayHandler;
	}
	
	public delegate IntPtr GetLifeSpanHandlerCallback(IntPtr self);
	public delegate IntPtr GetLoadHandlerCallback(IntPtr self);
	public delegate IntPtr GetRequestHandlerCallback(IntPtr self);
	public delegate IntPtr GetDisplayHandlerCallback(IntPtr self);
	
}
