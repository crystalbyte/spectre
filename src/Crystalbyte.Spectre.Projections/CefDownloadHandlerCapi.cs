using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

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
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefDownloadHandlerCapiDelegates {
		public delegate void ContCallback4(IntPtr self, IntPtr downloadPath, int showDialog);
		public delegate void CancelCallback3(IntPtr self);
		public delegate void OnBeforeDownloadCallback(IntPtr self, IntPtr browser, IntPtr downloadItem, IntPtr suggestedName, IntPtr callback);
		public delegate void OnDownloadUpdatedCallback(IntPtr self, IntPtr browser, IntPtr downloadItem, IntPtr callback);
	}
	
}
