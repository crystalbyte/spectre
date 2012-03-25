using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefResourceHandler {
		CefBase Base;
		IntPtr ProcessRequest;
		IntPtr GetResponseHeaders;
		IntPtr ReadResponse;
		IntPtr Cancel;
	}
	
	public delegate int ProcessRequestCallback(IntPtr self, IntPtr request, IntPtr callback);
	public delegate void GetResponseHeadersCallback(IntPtr self, IntPtr response, IntPtr responseLength, IntPtr redirecturl);
	public delegate int ReadResponseCallback(IntPtr self, IntPtr dataOut, int bytesToRead, IntPtr bytesRead, IntPtr callback);
	
}
