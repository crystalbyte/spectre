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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class GeolocationHandler : OwnedRefCountedNativeTypeAdapter {
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
