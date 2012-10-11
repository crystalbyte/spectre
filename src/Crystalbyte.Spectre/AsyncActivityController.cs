#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class AsyncActivityController : RefCountedNativeObject{
        private AsyncActivityController(IntPtr handle)
            : base(typeof (CefCallback)){
            NativeHandle = handle;
        }

        public bool IsCanceled { get; private set; }
        public bool IsPaused { get; private set; }

        public static AsyncActivityController FromHandle(IntPtr handle){
            return new AsyncActivityController(handle);
        }

        public void Continue(){
            var r = MarshalFromNative<CefCallback>();
            var action = (ContCallback) Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (ContCallback));
            action(NativeHandle);
        }

        public void Cancel(){
            var r = MarshalFromNative<CefCallback>();
            var action = (CancelCallback) Marshal.GetDelegateForFunctionPointer(r.Cancel, typeof (CancelCallback));
            action(NativeHandle);
            IsCanceled = true;
        }

        public void Pause(){
            IsPaused = true;
        }
    }
}