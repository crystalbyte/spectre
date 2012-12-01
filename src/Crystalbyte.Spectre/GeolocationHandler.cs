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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class GeolocationHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefGeolocationHandlerCapiDelegates.OnCancelGeolocationPermissionCallback
            _cancelGeolocationPermission;

        private readonly BrowserDelegate _delegate;

        private readonly CefGeolocationHandlerCapiDelegates.OnRequestGeolocationPermissionCallback
            _requestGeolocationPermission;

        public GeolocationHandler(BrowserDelegate @delegate)
            : base(typeof (CefGeolocationHandler)) {
            _delegate = @delegate;
            _cancelGeolocationPermission = OnCancelGeolocationRequest;
            _requestGeolocationPermission = OnRequestGeolocationPermission;
            MarshalToNative(new CefGeolocationHandler {
                Base = DedicatedBase,
                OnRequestGeolocationPermission =
                    Marshal.GetFunctionPointerForDelegate(
                        _requestGeolocationPermission),
                OnCancelGeolocationPermission =
                    Marshal.GetFunctionPointerForDelegate(
                        _cancelGeolocationPermission)
            });
        }

        private void OnCancelGeolocationRequest(IntPtr self, IntPtr browser, IntPtr requestingurl, int requestid) {
            var e = new GeolocationRequestCanceledEventArgs {
                Browser = Browser.FromHandle(browser),
                RequestingUrl = StringUtf16.ReadString(requestingurl),
                RequestId = requestid
            };
            _delegate.OnGeolocationRequestCanceled(e);
        }

        private void OnRequestGeolocationPermission(IntPtr self, IntPtr browser, IntPtr requestingurl, int requestid,
                                                    IntPtr callback) {
            var e = new GeolocationRequestedEventArgs {
                Browser = Browser.FromHandle(browser),
                RequestingUrl = StringUtf16.ReadString(requestingurl),
                RequestId = requestid,
                Request = GeolocationRequest.FromHandle(callback)
            };
            _delegate.OnGeolocationRequested(e);
        }
    }
}
