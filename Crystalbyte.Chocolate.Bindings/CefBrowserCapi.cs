using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefBrowserCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_browser_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefBrowserCreate(IntPtr windowinfo, IntPtr client, IntPtr url, IntPtr settings);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_browser_create_sync", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefBrowserCreateSync(IntPtr windowinfo, IntPtr client, IntPtr url, IntPtr settings);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBrowser {
		public CefBase Base;
		public IntPtr ParentWindowWillClose;
		public IntPtr CloseBrowser;
		public IntPtr CanGoBack;
		public IntPtr GoBack;
		public IntPtr CanGoForward;
		public IntPtr GoForward;
		public IntPtr IsLoading;
		public IntPtr Reload;
		public IntPtr ReloadIgnoreCache;
		public IntPtr StopLoad;
		public IntPtr SetFocus;
		public IntPtr GetWindowHandle;
		public IntPtr GetOpenerWindowHandle;
		public IntPtr IsPopup;
		public IntPtr HasDocument;
		public IntPtr GetClient;
		public IntPtr GetMainFrame;
		public IntPtr GetFocusedFrame;
		public IntPtr GetFrameByident;
		public IntPtr GetFrame;
		public IntPtr GetFrameCount;
		public IntPtr GetFrameIdentifiers;
		public IntPtr GetFrameNames;
		public IntPtr Find;
		public IntPtr StopFinding;
		public IntPtr GetZoomLevel;
		public IntPtr SetZoomLevel;
		public IntPtr ClearHistory;
		public IntPtr ShowDevTools;
		public IntPtr CloseDevTools;
	}
	
	public delegate void ParentWindowWillCloseCallback(IntPtr self);
	public delegate void CloseBrowserCallback(IntPtr self);
	public delegate int CanGoBackCallback(IntPtr self);
	public delegate void GoBackCallback(IntPtr self);
	public delegate int CanGoForwardCallback(IntPtr self);
	public delegate void GoForwardCallback(IntPtr self);
	public delegate int IsLoadingCallback(IntPtr self);
	public delegate void ReloadCallback(IntPtr self);
	public delegate void ReloadIgnoreCacheCallback(IntPtr self);
	public delegate void StopLoadCallback(IntPtr self);
	public delegate void SetFocusCallback(IntPtr self, int enable);
	public delegate IntPtr GetWindowHandleCallback(IntPtr self);
	public delegate IntPtr GetOpenerWindowHandleCallback(IntPtr self);
	public delegate int IsPopupCallback(IntPtr self);
	public delegate int HasDocumentCallback(IntPtr self);
	public delegate IntPtr GetClientCallback(IntPtr self);
	public delegate IntPtr GetMainFrameCallback(IntPtr self);
	public delegate IntPtr GetFocusedFrameCallback(IntPtr self);
	public delegate IntPtr GetFrameByidentCallback(IntPtr self, long identifier);
	public delegate IntPtr GetFrameCallback(IntPtr self, IntPtr name);
	public delegate int GetFrameCountCallback(IntPtr self);
	public delegate void GetFrameIdentifiersCallback(IntPtr self, IntPtr identifierscount, IntPtr identifiers);
	public delegate void GetFrameNamesCallback(IntPtr self, IntPtr names);
	public delegate void FindCallback(IntPtr self, int identifier, IntPtr searchtext, int forward, int matchcase, int findnext);
	public delegate void StopFindingCallback(IntPtr self, int clearselection);
	public delegate Double GetZoomLevelCallback(IntPtr self);
	public delegate void SetZoomLevelCallback(IntPtr self, Double zoomlevel);
	public delegate void ClearHistoryCallback(IntPtr self);
	public delegate void ShowDevToolsCallback(IntPtr self);
	public delegate void CloseDevToolsCallback(IntPtr self);
	
}
