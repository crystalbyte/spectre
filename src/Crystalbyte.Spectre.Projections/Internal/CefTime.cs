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
}
