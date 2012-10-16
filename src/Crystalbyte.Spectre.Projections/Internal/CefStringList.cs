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

namespace Crystalbyte.Spectre.Projections.Internal {
    [SuppressUnmanagedCodeSecurity]
    public static class CefStringListClass {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_alloc", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStringListAlloc();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_size", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringListSize(IntPtr list);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_value", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringListValue(IntPtr list, int index, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_append", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefStringListAppend(IntPtr list, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_clear", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefStringListClear(IntPtr list);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_free", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefStringListFree(IntPtr list);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_list_copy", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStringListCopy(IntPtr list);
    }
}
