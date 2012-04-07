#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    internal static class Reference {
        public static void Decrement(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (ReleaseCallback) Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (ReleaseCallback));
            function(handle);
        }

        public static void Increment(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (AddRefCallback));
            function(handle);
        }

        public static int GetReferenceCounter(IntPtr handle){
            var obj = (CefBase)Marshal.PtrToStructure(handle, typeof(CefBase));
            var function =
                (AddRefCallback)Marshal.GetDelegateForFunctionPointer(obj.GetRefct, typeof(AddRefCallback));
            return function(handle);
        }
    }
}