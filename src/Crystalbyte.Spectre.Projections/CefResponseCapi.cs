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
    public static class CefResponseCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_response_create", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefResponseCreate();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefResponse {
        public CefBase Base;
        public IntPtr IsReadOnly;
        public IntPtr GetStatus;
        public IntPtr SetStatus;
        public IntPtr GetStatusText;
        public IntPtr SetStatusText;
        public IntPtr GetMimeType;
        public IntPtr SetMimeType;
        public IntPtr GetHeader;
        public IntPtr GetHeaderMap;
        public IntPtr SetHeaderMap;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefResponseCapiDelegates {
        public delegate int IsReadOnlyCallback6(IntPtr self);

        public delegate int GetStatusCallback(IntPtr self);

        public delegate void SetStatusCallback(IntPtr self, int status);

        public delegate IntPtr GetStatusTextCallback(IntPtr self);

        public delegate void SetStatusTextCallback(IntPtr self, IntPtr statustext);

        public delegate IntPtr GetMimeTypeCallback2(IntPtr self);

        public delegate void SetMimeTypeCallback(IntPtr self, IntPtr mimetype);

        public delegate IntPtr GetHeaderCallback(IntPtr self, IntPtr name);

        public delegate void GetHeaderMapCallback2(IntPtr self, IntPtr headermap);

        public delegate void SetHeaderMapCallback2(IntPtr self, IntPtr headermap);
    }
}
