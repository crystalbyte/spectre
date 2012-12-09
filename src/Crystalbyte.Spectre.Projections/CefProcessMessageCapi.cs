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
    public static class CefProcessMessageCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_process_message_create",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr CefProcessMessageCreate(IntPtr name);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefProcessMessage {
        public CefBase Base;
        public IntPtr IsValid;
        public IntPtr IsReadOnly;
        public IntPtr Copy;
        public IntPtr GetName;
        public IntPtr GetArgumentList;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefProcessMessageCapiDelegates {
        public delegate int IsValidCallback4(IntPtr self);

        public delegate int IsReadOnlyCallback2(IntPtr self);

        public delegate IntPtr CopyCallback3(IntPtr self);

        public delegate IntPtr GetNameCallback3(IntPtr self);

        public delegate IntPtr GetArgumentListCallback(IntPtr self);
    }
}
