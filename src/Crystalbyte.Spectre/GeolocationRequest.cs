#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class GeolocationRequest : CefTypeAdapter {
        private GeolocationRequest(IntPtr handle)
            : base(typeof (CefGeolocationCallback)) {
            Handle = handle;
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
            action(Handle, allow);
            IsDecisionPending = false;
        }

        // Has not been auto generated, due to naming collision.

        #region Nested type: CefPermissionContCallback

        private delegate void CefPermissionContCallback(IntPtr self, int allow);

        #endregion
    }
}
