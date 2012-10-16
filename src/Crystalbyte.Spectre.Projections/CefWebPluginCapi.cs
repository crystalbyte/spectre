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

    public delegate IntPtr GetPathCallback(IntPtr self);

    public delegate IntPtr GetVersionCallback(IntPtr self);

    public delegate IntPtr GetDescriptionCallback(IntPtr self);

    public delegate void IsUnstableCallback(IntPtr self, IntPtr path, int unstable);
}
