#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class ResponseDelayController : RefCountedNativeObject{
        private ResponseDelayController(IntPtr handle)
            : base(typeof (CefCallback)){
            NativeHandle = handle;
        }

        public bool IsPaused { get; private set; }

        public static ResponseDelayController FromHandle(IntPtr handle){
            return new ResponseDelayController(handle);
        }

        public void Resume(){
            var r = MarshalFromNative<CefCallback>();
            var action = (ContCallback) Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (ContCallback));
            action(NativeHandle);
        }

        public void Pause(){
            IsPaused = true;
        }
    }
}