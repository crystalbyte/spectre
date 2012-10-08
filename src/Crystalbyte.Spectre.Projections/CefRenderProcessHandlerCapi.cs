using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefRenderProcessHandler {
		public CefBase Base;
		public IntPtr OnRenderThreadCreated;
		public IntPtr OnWebKitInitialized;
		public IntPtr OnBrowserCreated;
		public IntPtr OnBrowserDestroyed;
		public IntPtr OnContextCreated;
		public IntPtr OnContextReleased;
		public IntPtr OnFocusedNodeChanged;
		public IntPtr OnProcessMessageReceived;
	}
	
	public delegate void OnRenderThreadCreatedCallback(IntPtr self);
	public delegate void OnWebKitInitializedCallback(IntPtr self);
	public delegate void OnBrowserCreatedCallback(IntPtr self, IntPtr browser);
	public delegate void OnBrowserDestroyedCallback(IntPtr self, IntPtr browser);
	public delegate void OnContextCreatedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);
	public delegate void OnContextReleasedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context);
	public delegate void OnFocusedNodeChangedCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr node);
	
}
