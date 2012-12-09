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

    [SuppressUnmanagedCodeSecurity]
    public static class CefSchemeCapiDelegates {
        public delegate int AddCustomSchemeCallback(
            IntPtr self, IntPtr schemeName, int isStandard, int isLocal, int isDisplayIsolated);

        public delegate IntPtr CreateCallback(
            IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemeName, IntPtr request);
    }
}
