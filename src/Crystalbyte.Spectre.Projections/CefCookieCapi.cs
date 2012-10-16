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

#endregion

namespace Crystalbyte.Spectre.Projections {
    [SuppressUnmanagedCodeSecurity]
    public static class CefCookieCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_cookie_manager_get_global_manager",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefCookieManagerGetGlobalManager();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_cookie_manager_create_manager",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefCookieManagerCreateManager(IntPtr path);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefCookieManager {
        public CefBase Base;
        public IntPtr SetSupportedSchemes;
        public IntPtr VisitAllCookies;
        public IntPtr VisitUrlCookies;
        public IntPtr SetCookie;
        public IntPtr DeleteCookies;
        public IntPtr SetStoragePath;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefCookieVisitor {
        public CefBase Base;
        public IntPtr Visit;
    }

    public delegate void SetSupportedSchemesCallback(IntPtr self, IntPtr schemes);

    public delegate int VisitAllCookiesCallback(IntPtr self, IntPtr visitor);

    public delegate int VisitUrlCookiesCallback(IntPtr self, IntPtr url, int includehttponly, IntPtr visitor);

    public delegate int SetCookieCallback(IntPtr self, IntPtr url, IntPtr cookie);

    public delegate int DeleteCookiesCallback(IntPtr self, IntPtr url, IntPtr cookieName);

    public delegate int SetStoragePathCallback(IntPtr self, IntPtr path);

    public delegate int VisitCallback(IntPtr self, IntPtr cookie, int count, int total, IntPtr deletecookie);
}
