using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProxyHandler {
		public CefBase Base;
		public IntPtr GetProxyForUrl;
	}
	
	public delegate void GetProxyForUrlCallback(IntPtr self, IntPtr url, IntPtr proxyInfo);
	
}
