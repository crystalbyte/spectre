﻿#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class LoadHandler : OwnedAdapter {
        private readonly BrowserDelegate _delegate;
        private readonly OnLoadEndCallback _loadEndCallback;
        private readonly OnLoadErrorCallback _loadErrorCallback;
        private readonly OnLoadStartCallback _loadStartCallback;

        public LoadHandler(BrowserDelegate @delegate)
            : base(typeof (CefLoadHandler)) {
            _delegate = @delegate;
            _loadEndCallback = OnLoadEnd;
            _loadStartCallback = OnLoadStart;
            _loadErrorCallback = OnLoadError;

            MarshalToNative(new CefLoadHandler {
                Base = DedicatedBase,
                OnLoadEnd = Marshal.GetFunctionPointerForDelegate(_loadEndCallback),
                OnLoadStart = Marshal.GetFunctionPointerForDelegate(_loadStartCallback),
                OnLoadError = Marshal.GetFunctionPointerForDelegate(_loadErrorCallback)
            });
        }

        private void OnLoadError(IntPtr self, IntPtr browser, IntPtr frame, CefErrorcode errorcode,
                                 IntPtr errortext, IntPtr failedurl) {
            var e = new PageLoadingFailedEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                ErrorCode = (ErrorCode) errorcode,
                Message = StringUtf16.ReadStringAndFree(errortext),
                FailedUrl = StringUtf16.ReadStringAndFree(failedurl)
            };
            _delegate.OnPageLoadingFailed(e);
        }

        private void OnLoadStart(IntPtr self, IntPtr browser, IntPtr frame) {
            var e = new PageLoadingEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame)
            };
            _delegate.OnPageLoading(e);
        }

        private void OnLoadEnd(IntPtr self, IntPtr browser, IntPtr frame, int httpstatuscode) {
            var e = new PageLoadedEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                HttpStatusCode = httpstatuscode
            };
            _delegate.OnPageLoaded(e);
        }
    }
}