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

#endregion

namespace Crystalbyte.Spectre.Projections {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefAuthCallback {
        public CefBase Base;
        public IntPtr Cont;
        public IntPtr Cancel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefQuotaCallback {
        public CefBase Base;
        public IntPtr Cont;
        public IntPtr Cancel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefRequestHandler {
        public CefBase Base;
        public IntPtr OnBeforeResourceLoad;
        public IntPtr GetResourceHandler;
        public IntPtr OnResourceRedirect;
        public IntPtr GetAuthCredentials;
        public IntPtr OnQuotaRequest;
        public IntPtr GetCookieManager;
        public IntPtr OnProtocolExecution;
        public IntPtr OnBeforePluginLoad;
    }

    public delegate int OnBeforeResourceLoadCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);

    public delegate IntPtr GetResourceHandlerCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr request);

    public delegate void OnResourceRedirectCallback(
        IntPtr self, IntPtr browser, IntPtr frame, IntPtr oldUrl, IntPtr newUrl);

    public delegate int GetAuthCredentialsCallback(
        IntPtr self, IntPtr browser, IntPtr frame, int isproxy, IntPtr host, int port, IntPtr realm, IntPtr scheme,
        IntPtr callback);

    public delegate int OnQuotaRequestCallback(
        IntPtr self, IntPtr browser, IntPtr originUrl, long newSize, IntPtr callback);

    public delegate IntPtr GetCookieManagerCallback(IntPtr self, IntPtr browser, IntPtr mainUrl);

    public delegate void OnProtocolExecutionCallback(IntPtr self, IntPtr browser, IntPtr url, IntPtr allowOsExecution);

    public delegate int OnBeforePluginLoadCallback(
        IntPtr self, IntPtr browser, IntPtr url, IntPtr policyUrl, IntPtr info);
}
