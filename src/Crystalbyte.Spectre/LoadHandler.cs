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
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class LoadHandler : OwnedRefCountedNativeObject {
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
