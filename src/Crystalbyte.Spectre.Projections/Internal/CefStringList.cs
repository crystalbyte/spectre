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
