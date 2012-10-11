#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Interop {
    internal static class Reference{
        public static void Decrement(IntPtr handle){
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (ReleaseCallback) Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (ReleaseCallback));
            function(handle);
        }

        public static void Decrement(RefCountedNativeObject item) {
            var obj = (CefBase)Marshal.PtrToStructure(item.NativeHandle, typeof(CefBase));
            var function =
                (ReleaseCallback)Marshal.GetDelegateForFunctionPointer(obj.Release, typeof(ReleaseCallback));
            function(item.NativeHandle);
        }

        public static void Increment(RefCountedNativeObject item) {
            var obj = (CefBase)Marshal.PtrToStructure(item.NativeHandle, typeof(CefBase));
            var function =
                (AddRefCallback)Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof(AddRefCallback));
            function(item.NativeHandle);
        }

        public static void Increment(IntPtr handle){
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (AddRefCallback));
            function(handle);
        }

        public static int GetReferenceCounter(IntPtr handle){
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.GetRefct, typeof (AddRefCallback));
            return function(handle);
        }
    }
}