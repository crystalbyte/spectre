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
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class LifeSpanHandler : RefCountedNativeObject {
        private readonly OnBeforeCloseCallback _beforeCloseCallback;
        private readonly OnBeforePopupCallback _beforePopupCallback;
        private readonly BrowserDelegate _delegate;
        private readonly DoCloseCallback _doCloseCallback;

        public LifeSpanHandler(BrowserDelegate @delegate)
            : base(typeof (CefLifeSpanHandler)) {
            _delegate = @delegate;
            _doCloseCallback = OnDoClose;
            _beforePopupCallback = OnBeforePopup;
            _beforeCloseCallback = OnBeforeClose;
            MarshalToNative(new CefLifeSpanHandler {
                Base = DedicatedBase,
                OnBeforePopup = Marshal.GetFunctionPointerForDelegate(_beforePopupCallback),
                OnBeforeClose = Marshal.GetFunctionPointerForDelegate(_beforeCloseCallback),
                DoClose = Marshal.GetFunctionPointerForDelegate(_doCloseCallback)
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

            // Need to call Dispose manually, since the GC will not be able to free the browser instance b above, it is still being referenced by local scope.
            b.Dispose();

            //// CEF requires all objects to be freed before the window is actually closed.
            //// Since this is a non recurring event which happens only when a window is closed the GC calls should not affect performance.
            //// Let's not poke the GC more than required ;)
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