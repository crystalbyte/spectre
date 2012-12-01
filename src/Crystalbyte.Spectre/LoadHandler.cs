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
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class LoadHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefLoadHandlerCapiDelegates.OnLoadEndCallback _loadEndCallback;
        private readonly CefLoadHandlerCapiDelegates.OnLoadErrorCallback _loadErrorCallback;
        private readonly CefLoadHandlerCapiDelegates.OnLoadStartCallback _loadStartCallback;
        private readonly BrowserDelegate _delegate;

        public LoadHandler(BrowserDelegate @delegate)
            : base(typeof (CefLoadHandler)) {
            _delegate = @delegate;
            _loadEndCallback = OnLoadEnd;
            _loadStartCallback = OnLoadStart;
            _loadErrorCallback = OnLoadError;

            MarshalToNative(new CefLoadHandler {
                Base = DedicatedBase,
                OnLoadEnd = Marshal.GetFunctionPointerForDelegate(_loadEndCallback),
                OnLoadStart =
                    Marshal.GetFunctionPointerForDelegate(_loadStartCallback),
                OnLoadError =
                    Marshal.GetFunctionPointerForDelegate(_loadErrorCallback)
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
