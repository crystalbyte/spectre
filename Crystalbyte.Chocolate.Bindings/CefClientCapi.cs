using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefClient {
		public CefBase Base;
		public IntPtr GetLifeSpanHandler;
		public IntPtr GetLoadHandler;
		public IntPtr GetRequestHandler;
		public IntPtr GetDisplayHandler;
	}
	
	public delegate IntPtr GetLifeSpanHandlerCallback(IntPtr self);
	public delegate IntPtr GetLoadHandlerCallback(IntPtr self);
	public delegate IntPtr GetRequestHandlerCallback(IntPtr self);
	public delegate IntPtr GetDisplayHandlerCallback(IntPtr self);
	
}
