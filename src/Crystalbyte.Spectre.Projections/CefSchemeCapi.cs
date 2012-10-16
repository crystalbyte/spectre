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
    public static class CefSchemeCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_register_scheme_handler_factory",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefRegisterSchemeHandlerFactory(IntPtr schemeName, IntPtr domainName, IntPtr factory);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_clear_scheme_handler_factories",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int CefClearSchemeHandlerFactories();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefSchemeRegistrar {
        public CefBase Base;
        public IntPtr AddCustomScheme;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefSchemeHandlerFactory {
        public CefBase Base;
        public IntPtr Create;
    }

    public delegate int AddCustomSchemeCallback(
        IntPtr self, IntPtr schemeName, int isStandard, int isLocal, int isDisplayIsolated);

    public delegate IntPtr CreateCallback(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemeName, IntPtr request);
}
