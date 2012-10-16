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

    public delegate int GetStatusCallback(IntPtr self);

    public delegate void SetStatusCallback(IntPtr self, int status);

    public delegate IntPtr GetStatusTextCallback(IntPtr self);

    public delegate void SetStatusTextCallback(IntPtr self, IntPtr statustext);

    public delegate void SetMimeTypeCallback(IntPtr self, IntPtr mimetype);

    public delegate IntPtr GetHeaderCallback(IntPtr self, IntPtr name);
}
