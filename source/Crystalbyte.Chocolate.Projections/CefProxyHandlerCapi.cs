using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProxyHandler {
		public CefBase Base;
		public IntPtr GetProxyForUrl;
	}
	
	public delegate void GetProxyForUrlCallback(IntPtr self, IntPtr url, IntPtr proxyInfo);
	
}
