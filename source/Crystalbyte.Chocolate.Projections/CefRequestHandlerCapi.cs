using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefAuthCallback {
		public CefBase Base;
		public IntPtr Cont;
		public IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefQuotaCallback {
		public CefBase Base;
		public IntPtr Cont;
		public IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefRequestHandler {
		public CefBase Base;
		public IntPtr OnBeforeResourceLoad;
		public IntPtr GetResourceHandler;
		public IntPtr OnResourceRedirect;
		public IntPtr GetAuthCredentials;
		public IntPtr OnQuotaRequest;
		public IntPtr GetCookieManager;
		public IntPtr OnProtocolExecution;
		public IntPtr OnBeforePluginLoad;
	}
	
	public delegate int OnBeforeResourceLoadCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);
	public delegate IntPtr GetResourceHandlerCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);
	public delegate void OnResourceRedirectCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr oldUrl, IntPtr newUrl);
	public delegate int GetAuthCredentialsCallback(IntPtr self, IntPtr browser, IntPtr frame, int isproxy, IntPtr host, int port, IntPtr realm, IntPtr scheme, IntPtr callback);
	public delegate int OnQuotaRequestCallback(IntPtr self, IntPtr browser, IntPtr originUrl, long newSize, IntPtr callback);
	public delegate IntPtr GetCookieManagerCallback(IntPtr self, IntPtr browser, IntPtr mainUrl);
	public delegate void OnProtocolExecutionCallback(IntPtr self, IntPtr browser, IntPtr url, IntPtr allowOsExecution);
	public delegate int OnBeforePluginLoadCallback(IntPtr self, IntPtr browser, IntPtr url, IntPtr policyUrl, IntPtr info);
	
}
