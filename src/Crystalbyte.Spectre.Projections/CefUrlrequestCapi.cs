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
    public static class CefUrlrequestCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_urlrequest_create", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefUrlrequestCreate(IntPtr request, IntPtr client);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefUrlrequest {
        public CefBase Base;
        public IntPtr GetRequest;
        public IntPtr GetClient;
        public IntPtr GetRequestStatus;
        public IntPtr GetRequestError;
        public IntPtr GetResponse;
        public IntPtr Cancel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefUrlrequestClient {
        public CefBase Base;
        public IntPtr OnRequestComplete;
        public IntPtr OnUploadProgress;
        public IntPtr OnDownloadProgress;
        public IntPtr OnDownloadData;
    }

    public delegate IntPtr GetRequestCallback(IntPtr self);

    public delegate CefUrlrequestStatus GetRequestStatusCallback(IntPtr self);

    public delegate CefErrorcode GetRequestErrorCallback(IntPtr self);

    public delegate IntPtr GetResponseCallback(IntPtr self);

    public delegate void OnRequestCompleteCallback(IntPtr self, IntPtr request);

    public delegate void OnUploadProgressCallback(IntPtr self, IntPtr request, ulong current, ulong total);

    public delegate void OnDownloadProgressCallback(IntPtr self, IntPtr request, ulong current, ulong total);

    public delegate void OnDownloadDataCallback(IntPtr self, IntPtr request, IntPtr data, int dataLength);
}
