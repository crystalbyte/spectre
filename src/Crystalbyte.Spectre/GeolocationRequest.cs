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
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class GeolocationRequest : NativeObject {
        private GeolocationRequest(IntPtr handle)
            : base(typeof (CefGeolocationCallback)) {
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

        // Has not been auto generated, due to naming collision.

        #region Nested type: CefPermissionContCallback

        private delegate void CefPermissionContCallback(IntPtr self, int allow);

        #endregion
    }
}
