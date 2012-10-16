#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Projections {
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
        public IntPtr SendProcessMessage;
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
        public IntPtr GetDevToolsUrl;
        public IntPtr GetZoomLevel;
        public IntPtr SetZoomLevel;
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

    public delegate void GetFrameIdentifiersCallback(IntPtr self, out long identifierscount, IntPtr identifiers);

    public delegate void GetFrameNamesCallback(IntPtr self, IntPtr names);

    public delegate int SendProcessMessageCallback(IntPtr self, CefProcessId targetProcess, IntPtr message);

    public delegate IntPtr GetBrowserCallback(IntPtr self);

    public delegate void ParentWindowWillCloseCallback(IntPtr self);

    public delegate void CloseBrowserCallback(IntPtr self);

    public delegate void SetFocusCallback(IntPtr self, int enable);

    public delegate IntPtr GetWindowHandleCallback(IntPtr self);

    public delegate IntPtr GetOpenerWindowHandleCallback(IntPtr self);

    public delegate IntPtr GetClientCallback(IntPtr self);

    public delegate IntPtr GetDevToolsUrlCallback(IntPtr self, int httpScheme);

    public delegate Double GetZoomLevelCallback(IntPtr self);

    public delegate void SetZoomLevelCallback(IntPtr self, Double zoomlevel);
}
