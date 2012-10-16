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
