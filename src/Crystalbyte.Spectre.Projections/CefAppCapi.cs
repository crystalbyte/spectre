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
    public static class CefAppCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_execute_process", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefExecuteProcess(IntPtr args, IntPtr application);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_initialize", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefInitialize(IntPtr args, IntPtr settings, IntPtr application);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_shutdown", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefShutdown();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_do_message_loop_work",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern void CefDoMessageLoopWork();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_run_message_loop", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefRunMessageLoop();

        [DllImport(CefAssembly.Name, EntryPoint = "cef_quit_message_loop", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern void CefQuitMessageLoop();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefApp {
        public CefBase Base;
        public IntPtr OnBeforeCommandLineProcessing;
        public IntPtr OnRegisterCustomSchemes;
        public IntPtr CefCallbackGetResourceBundleHandler;
        public IntPtr CefCallbackGetBrowserProcessHandler;
        public IntPtr CefCallbackGetRenderProcessHandler;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefAppCapiDelegates {
        public delegate void OnBeforeCommandLineProcessingCallback(IntPtr self, IntPtr processType, IntPtr commandLine);

        public delegate void OnRegisterCustomSchemesCallback(IntPtr self, IntPtr registrar);

        public delegate IntPtr GetResourceBundleHandlerCallback(IntPtr self);

        public delegate IntPtr GetBrowserProcessHandlerCallback(IntPtr self);

        public delegate IntPtr GetRenderProcessHandlerCallback(IntPtr self);
    }
}
