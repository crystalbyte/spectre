using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;
using System.Runtime.InteropServices;

namespace Crystalbyte.Chocolate
{
    public sealed class GeolocationRequest : Adapter {

        // is not auto generated due to naming collision
        private delegate void CefPermissionContCallback(IntPtr self, int allow);

        private GeolocationRequest(IntPtr handle) 
            : base(typeof(CefGeolocationCallback), true) {
            NativeHandle = handle;
            IsDecisionPending = true;
        }

        public static GeolocationRequest FromHandle(IntPtr handle) {
            return new GeolocationRequest(handle);
        }

        public bool IsDecisionPending { get; private set; }

        public void Allow() {
            Continue(1);
            IsDecisionPending = false;
        }

        public void Deny(){
            Continue(0);
            IsDecisionPending = false;
        }

        private void Continue(int allow) {
            var r = MarshalFromNative<CefGeolocationCallback>();
            var action = (CefPermissionContCallback)Marshal.GetDelegateForFunctionPointer(r.Cont, typeof(CefPermissionContCallback));
            action(NativeHandle, allow);
            IsDecisionPending = false;
        }
    }
}
