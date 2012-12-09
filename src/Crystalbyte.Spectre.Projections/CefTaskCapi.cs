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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [SuppressUnmanagedCodeSecurity]
    public static class CefTaskCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_currently_on", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefCurrentlyOn(CefThreadId threadid);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_task", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefPostTask(CefThreadId threadid, IntPtr task);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_post_delayed_task", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefPostDelayedTask(CefThreadId threadid, IntPtr task, long delayMs);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefTask {
        public CefBase Base;
        public IntPtr Execute;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefTaskCapiDelegates {
        public delegate void ExecuteCallback(IntPtr self, CefThreadId threadid);
    }
}
