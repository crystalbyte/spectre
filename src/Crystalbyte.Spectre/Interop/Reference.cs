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
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Interop {
    internal static class Reference {
        public static void Decrement(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (CefBaseCapiDelegates.ReleaseCallback)
                Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (CefBaseCapiDelegates.ReleaseCallback));
            function(handle);
        }

        public static void Decrement(RefCountedCefTypeAdapter item) {
            var obj = (CefBase) Marshal.PtrToStructure(item.Handle, typeof (CefBase));
            var function =
                (CefBaseCapiDelegates.ReleaseCallback)
                Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (CefBaseCapiDelegates.ReleaseCallback));
            function(item.Handle);
        }

        public static void Increment(RefCountedCefTypeAdapter item) {
            var obj = (CefBase) Marshal.PtrToStructure(item.Handle, typeof (CefBase));
            var function =
                (CefBaseCapiDelegates.AddRefCallback)
                Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (CefBaseCapiDelegates.AddRefCallback));
            function(item.Handle);
        }

        public static void Increment(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (CefBaseCapiDelegates.AddRefCallback)
                Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (CefBaseCapiDelegates.AddRefCallback));
            function(handle);
        }

        public static int GetReferenceCounter(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (CefBaseCapiDelegates.AddRefCallback)
                Marshal.GetDelegateForFunctionPointer(obj.GetRefct, typeof (CefBaseCapiDelegates.AddRefCallback));
            return function(handle);
        }
    }
}
