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
    public static class CefTimeClass {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_timet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeToTimet(IntPtr cefTime, IntPtr time);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_timet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeFromTimet(long time, IntPtr cefTime);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_to_doublet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeToDoublet(IntPtr cefTime, IntPtr time);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_from_doublet", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeFromDoublet(Double time, IntPtr cefTime);

        [DllImport(CefAssembly.Name, EntryPoint = "cef_time_now", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern int CefTimeNow(IntPtr cefTime);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefTime {
        public int Year;
        public int Month;
        public int DayOfWeek;
        public int DayOfMonth;
        public int Hour;
        public int Minute;
        public int Second;
        public int Millisecond;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefTimeDelegates {}
}
