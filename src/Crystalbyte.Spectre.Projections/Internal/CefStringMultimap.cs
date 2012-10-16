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
    public static class CefStringMultimapClass {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_alloc",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStringMultimapAlloc();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_size",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapSize(IntPtr map);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_find_count",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapFindCount(IntPtr map, IntPtr key);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_erate",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapErate(IntPtr map, IntPtr key, int valueIndex, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_key", CallingConvention = CallingConvention.Cdecl
            , CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapKey(IntPtr map, int index, IntPtr key);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_value",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapValue(IntPtr map, int index, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_append",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefStringMultimapAppend(IntPtr map, IntPtr key, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_clear",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefStringMultimapClear(IntPtr map);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_multimap_free",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefStringMultimapFree(IntPtr map);
    }
}
