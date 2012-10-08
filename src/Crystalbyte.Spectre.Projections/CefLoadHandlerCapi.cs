using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefLoadHandler {
		public CefBase Base;
		public IntPtr OnLoadStart;
		public IntPtr OnLoadEnd;
		public IntPtr OnLoadError;
		public IntPtr OnRenderProcessTerminated;
		public IntPtr OnPluginCrashed;
	}
	
	public delegate void OnLoadStartCallback(IntPtr self, IntPtr browser, IntPtr frame);
	public delegate void OnLoadEndCallback(IntPtr self, IntPtr browser, IntPtr frame, int httpstatuscode);
	public delegate void OnLoadErrorCallback(IntPtr self, IntPtr browser, IntPtr frame, CefErrorcode errorcode, IntPtr errortext, IntPtr failedurl);
	public delegate void OnRenderProcessTerminatedCallback(IntPtr self, IntPtr browser, CefTerminationStatus status);
	public delegate void OnPluginCrashedCallback(IntPtr self, IntPtr browser, IntPtr pluginPath);
	
}
