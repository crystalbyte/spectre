using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBrowserProcessHandler {
		public CefBase Base;
		public IntPtr GetProxyHandler;
		public IntPtr OnContextInitialized;
	}
	
	public delegate IntPtr GetProxyHandlerCallback(IntPtr self);
	public delegate void OnContextInitializedCallback(IntPtr self);
	
}
