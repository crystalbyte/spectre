#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Chocolate.Bindings {
    [SuppressUnmanagedCodeSecurity]
    public static class CefBrowserCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_browser_host_create_browser",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefBrowserHostCreateBrowser(IntPtr windowinfo, IntPtr client, IntPtr url,
                                                             IntPtr settings);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_browser_host_create_browser_sync",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefBrowserHostCreateBrowserSync(IntPtr windowinfo, IntPtr client, IntPtr url,
                                                                    IntPtr settings);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefBrowser {
        public CefBase Base;
        public IntPtr GetHost;
        public IntPtr CanGoBack;
        public IntPtr GoBack;
        public IntPtr CanGoForward;
        public IntPtr GoForward;
        public IntPtr IsLoading;
        public IntPtr Reload;
        public IntPtr ReloadIgnoreCache;
        public IntPtr StopLoad;
        public IntPtr GetIdentifier;
        public IntPtr IsPopup;
        public IntPtr HasDocument;
        public IntPtr GetMainFrame;
        public IntPtr GetFocusedFrame;
        public IntPtr GetFrameByident;
        public IntPtr GetFrame;
        public IntPtr GetFrameCount;
        public IntPtr GetFrameIdentifiers;
        public IntPtr GetFrameNames;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefBrowserHost {
        public CefBase Base;
        public IntPtr GetBrowser;
        public IntPtr ParentWindowWillClose;
        public IntPtr CloseBrowser;
        public IntPtr SetFocus;
        public IntPtr GetWindowHandle;
        public IntPtr GetOpenerWindowHandle;
        public IntPtr GetClient;
    }

    public delegate IntPtr GetHostCallback(IntPtr self);

    public delegate int CanGoBackCallback(IntPtr self);

    public delegate void GoBackCallback(IntPtr self);

    public delegate int CanGoForwardCallback(IntPtr self);

    public delegate void GoForwardCallback(IntPtr self);

    public delegate int IsLoadingCallback(IntPtr self);

    public delegate void ReloadCallback(IntPtr self);

    public delegate void ReloadIgnoreCacheCallback(IntPtr self);

    public delegate void StopLoadCallback(IntPtr self);

    public delegate int GetIdentifierCallback(IntPtr self);

    public delegate int IsPopupCallback(IntPtr self);

    public delegate int HasDocumentCallback(IntPtr self);

    public delegate IntPtr GetMainFrameCallback(IntPtr self);

    public delegate IntPtr GetFocusedFrameCallback(IntPtr self);

    public delegate IntPtr GetFrameByidentCallback(IntPtr self, long identifier);

    public delegate IntPtr GetFrameCallback(IntPtr self, IntPtr name);

    public delegate int GetFrameCountCallback(IntPtr self);

    public delegate void GetFrameIdentifiersCallback(IntPtr self, IntPtr identifierscount, IntPtr identifiers);

    public delegate void GetFrameNamesCallback(IntPtr self, IntPtr names);

    public delegate IntPtr GetBrowserCallback(IntPtr self);

    public delegate void ParentWindowWillCloseCallback(IntPtr self);

    public delegate void CloseBrowserCallback(IntPtr self);

    public delegate void SetFocusCallback(IntPtr self, int enable);

    public delegate IntPtr GetWindowHandleCallback(IntPtr self);

    public delegate IntPtr GetOpenerWindowHandleCallback(IntPtr self);

    public delegate IntPtr GetClientCallback(IntPtr self);
}