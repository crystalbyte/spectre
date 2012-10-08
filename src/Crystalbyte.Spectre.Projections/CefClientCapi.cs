using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefClient {
		public CefBase Base;
		public IntPtr GetContextMenuHandler;
		public IntPtr GetDisplayHandler;
		public IntPtr GetDownloadHandler;
		public IntPtr GetFocusHandler;
		public IntPtr GetGeolocationHandler;
		public IntPtr GetJsdialogHandler;
		public IntPtr GetKeyboardHandler;
		public IntPtr GetLifeSpanHandler;
		public IntPtr GetLoadHandler;
		public IntPtr GetRequestHandler;
		public IntPtr OnProcessMessageReceived;
	}
	
	public delegate IntPtr GetContextMenuHandlerCallback(IntPtr self);
	public delegate IntPtr GetDisplayHandlerCallback(IntPtr self);
	public delegate IntPtr GetDownloadHandlerCallback(IntPtr self);
	public delegate IntPtr GetFocusHandlerCallback(IntPtr self);
	public delegate IntPtr GetGeolocationHandlerCallback(IntPtr self);
	public delegate IntPtr GetJsdialogHandlerCallback(IntPtr self);
	public delegate IntPtr GetKeyboardHandlerCallback(IntPtr self);
	public delegate IntPtr GetLifeSpanHandlerCallback(IntPtr self);
	public delegate IntPtr GetLoadHandlerCallback(IntPtr self);
	public delegate IntPtr GetRequestHandlerCallback(IntPtr self);
	public delegate int OnProcessMessageReceivedCallback(IntPtr self, IntPtr browser, CefProcessId sourceProcess, IntPtr message);
	
}
