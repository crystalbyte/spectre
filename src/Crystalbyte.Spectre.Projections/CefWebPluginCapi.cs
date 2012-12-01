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
    public static class CefWebPluginCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_visit_web_plugin_info",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefVisitWebPluginInfo(IntPtr visitor);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_refresh_web_plugins", CallingConvention = CallingConvention.Cdecl
            , CharSet = CharSet.Unicode)]
        public static extern void CefRefreshWebPlugins();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_add_web_plugin_path", CallingConvention = CallingConvention.Cdecl
            , CharSet = CharSet.Unicode)]
        public static extern void CefAddWebPluginPath(IntPtr path);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_add_web_plugin_directory",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefAddWebPluginDirectory(IntPtr dir);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_remove_web_plugin_path",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefRemoveWebPluginPath(IntPtr path);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_unregister_internal_web_plugin",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefUnregisterInternalWebPlugin(IntPtr path);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_force_web_plugin_shutdown",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefForceWebPluginShutdown(IntPtr path);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_register_web_plugin_crash",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefRegisterWebPluginCrash(IntPtr path);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_is_web_plugin_unstable",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefIsWebPluginUnstable(IntPtr path, IntPtr callback);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefWebPluginInfo {
        public CefBase Base;
        public IntPtr GetName;
        public IntPtr GetPath;
        public IntPtr GetVersion;
        public IntPtr GetDescription;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefWebPluginInfoVisitor {
        public CefBase Base;
        public IntPtr Visit;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefWebPluginUnstableCallback {
        public CefBase Base;
        public IntPtr IsUnstable;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefWebPluginCapiDelegates {
        public delegate IntPtr GetNameCallback4(IntPtr self);

        public delegate IntPtr GetPathCallback(IntPtr self);

        public delegate IntPtr GetVersionCallback(IntPtr self);

        public delegate IntPtr GetDescriptionCallback(IntPtr self);

        public delegate int VisitCallback4(IntPtr self, IntPtr info, int count, int total);

        public delegate void IsUnstableCallback(IntPtr self, IntPtr path, int unstable);
    }
}
