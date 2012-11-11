using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefJsdialogCallback {
		public CefBase Base;
		public IntPtr Cont;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefJsdialogHandler {
		public CefBase Base;
		public IntPtr OnJsdialog;
		public IntPtr OnBeforeUnloadDialog;
		public IntPtr OnResetDialogState;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefJsdialogHandlerCapiDelegates {
		public delegate void ContCallback6(IntPtr self, int success, IntPtr userInput);
		public delegate int OnJsdialogCallback(IntPtr self, IntPtr browser, IntPtr originUrl, IntPtr acceptLang, CefJsdialogType dialogType, IntPtr messageText, IntPtr defaultPromptText, IntPtr callback, IntPtr suppressMessage);
		public delegate int OnBeforeUnloadDialogCallback(IntPtr self, IntPtr browser, IntPtr messageText, int isReload, IntPtr callback);
		public delegate void OnResetDialogStateCallback(IntPtr self, IntPtr browser);
	}
	
}
