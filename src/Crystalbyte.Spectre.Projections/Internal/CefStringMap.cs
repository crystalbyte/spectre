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

namespace Crystalbyte.Spectre.Projections.Internal {
    [SuppressUnmanagedCodeSecurity]
    public static class CefStringMapClass {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_alloc", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefStringMapAlloc();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_size", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringMapSize(IntPtr map);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_find", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringMapFind(IntPtr map, IntPtr key, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_key", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringMapKey(IntPtr map, int index, IntPtr key);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_value", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringMapValue(IntPtr map, int index, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_append", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefStringMapAppend(IntPtr map, IntPtr key, IntPtr value);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_clear", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefStringMapClear(IntPtr map);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_string_map_free", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefStringMapFree(IntPtr map);
    }
}
