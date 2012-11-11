using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefFileDialogCallback {
		public CefBase Base;
		public IntPtr Cont;
		public IntPtr Cancel;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefDialogHandler {
		public CefBase Base;
		public IntPtr OnFileDialog;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefDialogHandlerCapiDelegates {
		public delegate void ContCallback3(IntPtr self, IntPtr filePaths);
		public delegate int OnFileDialogCallback(IntPtr self, IntPtr browser, CefFileDialogMode mode, IntPtr title, IntPtr defaultFileName, IntPtr acceptTypes, IntPtr callback);
	}
	
}
