#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

    [SuppressUnmanagedCodeSecurity]
    public static class CefCookieCapiDelegates {
        public delegate void SetSupportedSchemesCallback(IntPtr self, IntPtr schemes);

        public delegate int VisitAllCookiesCallback(IntPtr self, IntPtr visitor);

        public delegate int VisitUrlCookiesCallback(IntPtr self, IntPtr url, int includehttponly, IntPtr visitor);

        public delegate int SetCookieCallback(IntPtr self, IntPtr url, IntPtr cookie);

        public delegate int DeleteCookiesCallback(IntPtr self, IntPtr url, IntPtr cookieName);

        public delegate int SetStoragePathCallback(IntPtr self, IntPtr path);

        public delegate int VisitCallback(IntPtr self, IntPtr cookie, int count, int total, ref int deletecookie);
    }
}
