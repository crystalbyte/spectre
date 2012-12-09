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
    public static class CefOriginWhitelistCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_add_cross_origin_whitelist_entry",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefAddCrossOriginWhitelistEntry(IntPtr sourceOrigin, IntPtr targetProtocol,
                                                                 IntPtr targetDomain, int allowTargetSubdomains);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_remove_cross_origin_whitelist_entry",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefRemoveCrossOriginWhitelistEntry(IntPtr sourceOrigin, IntPtr targetProtocol,
                                                                    IntPtr targetDomain, int allowTargetSubdomains);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_clear_cross_origin_whitelist",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefClearCrossOriginWhitelist();
    }
}
