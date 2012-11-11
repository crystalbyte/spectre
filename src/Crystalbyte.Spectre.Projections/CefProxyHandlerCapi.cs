using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProxyHandler {
		public CefBase Base;
		public IntPtr GetProxyForUrl;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefProxyHandlerCapiDelegates {
		public delegate void GetProxyForUrlCallback(IntPtr self, IntPtr url, IntPtr proxyInfo);
	}
	
}
