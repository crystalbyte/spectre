#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class GeolocationRequest : NativeObject{
        private GeolocationRequest(IntPtr handle)
            : base(typeof (CefGeolocationCallback)){
            NativeHandle = handle;
            IsDecisionPending = true;
        }

        public bool IsDecisionPending { get; private set; }

        public static GeolocationRequest FromHandle(IntPtr handle){
            return new GeolocationRequest(handle);
        }

        public void Allow(){
            Continue(1);
            IsDecisionPending = false;
        }

        public void Deny(){
            Continue(0);
            IsDecisionPending = false;
        }

        private void Continue(int allow){
            var r = MarshalFromNative<CefGeolocationCallback>();
            var action =
                (CefPermissionContCallback)
                Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (CefPermissionContCallback));
            action(NativeHandle, allow);
            IsDecisionPending = false;
        }

        // Has not been auto generated, due to naming collision.

        #region Nested type: CefPermissionContCallback

        private delegate void CefPermissionContCallback(IntPtr self, int allow);

        #endregion
    }
}