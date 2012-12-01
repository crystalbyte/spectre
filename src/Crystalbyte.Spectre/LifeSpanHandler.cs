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
    public sealed class LifeSpanHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefLifeSpanHandlerCapiDelegates.OnBeforeCloseCallback _beforeCloseCallback;
        private readonly CefLifeSpanHandlerCapiDelegates.OnBeforePopupCallback _beforePopupCallback;
        private readonly CefLifeSpanHandlerCapiDelegates.DoCloseCallback _doCloseCallback;
        private readonly BrowserDelegate _delegate;

        public LifeSpanHandler(BrowserDelegate @delegate)
            : base(typeof (CefLifeSpanHandler)) {
            _delegate = @delegate;
            _doCloseCallback = OnDoClose;
            _beforePopupCallback = OnBeforePopup;
            _beforeCloseCallback = OnBeforeClose;

            MarshalToNative(new CefLifeSpanHandler {
                Base = DedicatedBase,
                OnBeforePopup =
                    Marshal.GetFunctionPointerForDelegate(_beforePopupCallback),
                OnBeforeClose =
                    Marshal.GetFunctionPointerForDelegate(_beforeCloseCallback),
                DoClose = Marshal.GetFunctionPointerForDelegate(_doCloseCallback),
            });
        }

        private int OnDoClose(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserClosingEventArgs(b);
            _delegate.OnClosing(e);
            return e.IsCanceled ? 1 : 0;
        }

        private void OnBeforeClose(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserClosedEventArgs(b);
            _delegate.OnClosed(e);

            // Need to call Dispose manually, since the GC will not be able to free the browser instance b above, for it will be still referenced by local scope.
            b.Dispose();

            //// CEF requires all objects to be freed before the window is actually closed.
            //// Since this is a non recurring event, calling the GC should not affect performance, but reclaim tons of memory.
            //// http://blogs.msdn.com/b/ricom/archive/2004/11/29/271829.aspx
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private int OnBeforePopup(IntPtr self, IntPtr parentbrowser, IntPtr popupfeatures, IntPtr windowinfo, IntPtr url,
                                  IntPtr client, IntPtr settings) {
            var e = new PopupCreatingEventArgs {
                Parent = Browser.FromHandle(parentbrowser),
                Info = WindowsWindowInfo.FromHandle(windowinfo),
                Settings = BrowserSettings.FromHandle(settings),
                Address = StringUtf16.ReadString(url)
            };
            _delegate.OnPopupCreating(e);
            return e.IsCanceled ? 1 : 0;
        }
    }
}
