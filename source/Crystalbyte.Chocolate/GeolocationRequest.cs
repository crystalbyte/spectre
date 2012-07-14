#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class GeolocationRequest : Adapter {
        // is not auto generated due to naming collision

        private GeolocationRequest(IntPtr handle)
            : base(typeof (CefGeolocationCallback), true) {
            NativeHandle = handle;
            IsDecisionPending = true;
        }

        public bool IsDecisionPending { get; private set; }

        public static GeolocationRequest FromHandle(IntPtr handle) {
            return new GeolocationRequest(handle);
        }

        public void Allow() {
            Continue(1);
            IsDecisionPending = false;
        }

        public void Deny() {
            Continue(0);
            IsDecisionPending = false;
        }

        private void Continue(int allow) {
            var r = MarshalFromNative<CefGeolocationCallback>();
            var action =
                (CefPermissionContCallback)
                Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (CefPermissionContCallback));
            action(NativeHandle, allow);
            IsDecisionPending = false;
        }

        #region Nested type: CefPermissionContCallback

        private delegate void CefPermissionContCallback(IntPtr self, int allow);

        #endregion
    }
}