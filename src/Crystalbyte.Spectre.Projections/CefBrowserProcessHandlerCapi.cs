using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBrowserProcessHandler {
		public CefBase Base;
		public IntPtr GetProxyHandler;
		public IntPtr OnContextInitialized;
		public IntPtr OnBeforeChildProcessLaunch;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefBrowserProcessHandlerCapiDelegates {
		public delegate IntPtr GetProxyHandlerCallback(IntPtr self);
		public delegate void OnContextInitializedCallback(IntPtr self);
		public delegate void OnBeforeChildProcessLaunchCallback(IntPtr self, IntPtr commandLine);
	}
	
}
