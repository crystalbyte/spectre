using System;
using System.Runtime.InteropServices;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBeforeDownloadCallback {
		public CefBase Base;
		public IntPtr Cont;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefDownloadItemCallback {
		public CefBase Base;
		public IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefDownloadHandler {
		public CefBase Base;
		public IntPtr OnBeforeDownload;
		public IntPtr OnDownloadUpdated;
	}
	
	public delegate void OnBeforeDownloadCallback(IntPtr self, IntPtr browser, IntPtr downloadItem, IntPtr suggestedName, IntPtr callback);
	public delegate void OnDownloadUpdatedCallback(IntPtr self, IntPtr browser, IntPtr downloadItem, IntPtr callback);
	
}
