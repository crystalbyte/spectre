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
    public static class CefRequestCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_request_create", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefRequestCreate();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_data_create", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefPostDataCreate();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_data_element_create",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefPostDataElementCreate();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefRequest {
        public CefBase Base;
        public IntPtr IsReadOnly;
        public IntPtr GetUrl;
        public IntPtr SetUrl;
        public IntPtr GetMethod;
        public IntPtr SetMethod;
        public IntPtr GetPostData;
        public IntPtr SetPostData;
        public IntPtr GetHeaderMap;
        public IntPtr SetHeaderMap;
        public IntPtr Set;
        public IntPtr GetFlags;
        public IntPtr SetFlags;
        public IntPtr GetFirstPartyForCookies;
        public IntPtr SetFirstPartyForCookies;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefPostData {
        public CefBase Base;
        public IntPtr IsReadOnly;
        public IntPtr GetElementCount;
        public IntPtr GetElements;
        public IntPtr RemoveElement;
        public IntPtr AddElement;
        public IntPtr RemoveElements;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefPostDataElement {
        public CefBase Base;
        public IntPtr IsReadOnly;
        public IntPtr SetToEmpty;
        public IntPtr SetToFile;
        public IntPtr SetToBytes;
        public IntPtr GetElementType;
        public IntPtr GetFile;
        public IntPtr GetBytesCount;
        public IntPtr GetBytes;
    }

    public delegate void SetUrlCallback(IntPtr self, IntPtr url);

    public delegate IntPtr GetMethodCallback(IntPtr self);

    public delegate void SetMethodCallback(IntPtr self, IntPtr method);

    public delegate IntPtr GetPostDataCallback(IntPtr self);

    public delegate void SetPostDataCallback(IntPtr self, IntPtr postdata);

    public delegate void GetHeaderMapCallback(IntPtr self, IntPtr headermap);

    public delegate void SetHeaderMapCallback(IntPtr self, IntPtr headermap);

    public delegate void SetCallback(IntPtr self, IntPtr url, IntPtr method, IntPtr postdata, IntPtr headermap);

    public delegate int GetFlagsCallback(IntPtr self);

    public delegate void SetFlagsCallback(IntPtr self, int flags);

    public delegate IntPtr GetFirstPartyForCookiesCallback(IntPtr self);

    public delegate void SetFirstPartyForCookiesCallback(IntPtr self, IntPtr url);

    public delegate int GetElementCountCallback(IntPtr self);

    public delegate void GetElementsCallback(IntPtr self, IntPtr elementscount, IntPtr elements);

    public delegate int RemoveElementCallback(IntPtr self, IntPtr element);

    public delegate int AddElementCallback(IntPtr self, IntPtr element);

    public delegate void RemoveElementsCallback(IntPtr self);

    public delegate void SetToEmptyCallback(IntPtr self);

    public delegate void SetToFileCallback(IntPtr self, IntPtr filename);

    public delegate void SetToBytesCallback(IntPtr self, int size, IntPtr bytes);

    public delegate IntPtr GetFileCallback(IntPtr self);

    public delegate int GetBytesCountCallback(IntPtr self);

    public delegate int GetBytesCallback(IntPtr self, int size, IntPtr bytes);
}
