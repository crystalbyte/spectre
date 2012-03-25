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
		CefBase Base;
		IntPtr ParentWindowWillClose;
		IntPtr CloseBrowser;
		IntPtr CanGoBack;
		IntPtr GoBack;
		IntPtr CanGoForward;
		IntPtr GoForward;
		IntPtr IsLoading;
		IntPtr Reload;
		IntPtr ReloadIgnoreCache;
		IntPtr StopLoad;
		IntPtr SetFocus;
		IntPtr GetWindowHandle;
		IntPtr GetOpenerWindowHandle;
		IntPtr IsPopup;
		IntPtr HasDocument;
		IntPtr GetClient;
		IntPtr GetMainFrame;
		IntPtr GetFocusedFrame;
		IntPtr GetFrameByident;
		IntPtr GetFrame;
		IntPtr GetFrameCount;
		IntPtr GetFrameIdentifiers;
		IntPtr GetFrameNames;
		IntPtr Find;
		IntPtr StopFinding;
		IntPtr GetZoomLevel;
		IntPtr SetZoomLevel;
		IntPtr ClearHistory;
		IntPtr ShowDevTools;
		IntPtr CloseDevTools;
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
