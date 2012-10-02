using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefResourceHandler {
		public CefBase Base;
		public IntPtr ProcessRequest;
		public IntPtr GetResponseHeaders;
		public IntPtr ReadResponse;
		public IntPtr CanGetCookie;
		public IntPtr CanSetCookie;
		public IntPtr Cancel;
	}
	
	public delegate int ProcessRequestCallback(IntPtr self, IntPtr request, IntPtr callback);
	public delegate void GetResponseHeadersCallback(IntPtr self, IntPtr response, out int responseLength, IntPtr redirecturl);
	public delegate int ReadResponseCallback(IntPtr self, IntPtr dataOut, int bytesToRead, out int bytesRead, IntPtr callback);
	public delegate int CanGetCookieCallback(IntPtr self, IntPtr cookie);
	public delegate int CanSetCookieCallback(IntPtr self, IntPtr cookie);
	
}
