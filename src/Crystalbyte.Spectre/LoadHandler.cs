#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class LoadHandler : RetainedNativeObject {
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
                Message = StringUtf16.ReadString(errortext),
                FailedUrl = StringUtf16.ReadString(failedurl)
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