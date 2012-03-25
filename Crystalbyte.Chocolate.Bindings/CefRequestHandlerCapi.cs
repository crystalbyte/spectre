using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefAuthCallback {
		CefBase Base;
		IntPtr Cont;
		IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefRequestHandler {
		CefBase Base;
		IntPtr OnBeforeResourceLoad;
		IntPtr GetResourceHandler;
		IntPtr OnResourceRedirect;
		IntPtr GetAuthCredentials;
	}
	
	public delegate int OnBeforeResourceLoadCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);
	public delegate IntPtr GetResourceHandlerCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);
	public delegate void OnResourceRedirectCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr oldUrl, IntPtr newUrl);
	public delegate int GetAuthCredentialsCallback(IntPtr self, IntPtr browser, IntPtr frame, int isproxy, IntPtr host, int port, IntPtr realm, IntPtr scheme, IntPtr callback);
	
}
