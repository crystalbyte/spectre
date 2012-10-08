using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBrowserProcessHandler {
		public CefBase Base;
		public IntPtr GetProxyHandler;
		public IntPtr OnContextInitialized;
		public IntPtr OnBeforeChildProcessLaunch;
	}
	
	public delegate IntPtr GetProxyHandlerCallback(IntPtr self);
	public delegate void OnContextInitializedCallback(IntPtr self);
	public delegate void OnBeforeChildProcessLaunchCallback(IntPtr self, IntPtr commandLine);
	
}
