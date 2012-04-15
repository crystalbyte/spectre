using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate {
    internal sealed class GeolocationHandler : OwnedAdapter {
        private BrowserDelegate _delegate;
        private OnRequestGeolocationPermissionCallback _requestGeolocationPermission;
        private OnCancelGeolocationPermissionCallback _cancelGeolocationPermission;

        public GeolocationHandler(BrowserDelegate @delegate) 
            : base(typeof(CefGeolocationHandler)) {
            _delegate = @delegate;
            _cancelGeolocationPermission = OnCancelGeolocationRequest;
            _requestGeolocationPermission = OnRequestGeolocationPermission;
            MarshalToNative(new CefGeolocationHandler {
                Base = DedicatedBase,
                OnRequestGeolocationPermission = Marshal.GetFunctionPointerForDelegate(_requestGeolocationPermission),
                OnCancelGeolocationPermission = Marshal.GetFunctionPointerForDelegate(_cancelGeolocationPermission)
            });
        }

        private void OnCancelGeolocationRequest(IntPtr self, IntPtr browser, IntPtr requestingurl, int requestid) {
            var e = new GeolocationRequestCanceledEventArgs {
                Brower = Browser.FromHandle(browser),
                RequestingUrl = StringUtf16.ReadString(requestingurl),
                RequestId = requestid
            };
            _delegate.OnGeolocationRequestCanceled(e);
        }

        private void OnRequestGeolocationPermission(IntPtr self, IntPtr browser, IntPtr requestingurl, int requestid, IntPtr callback) {
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