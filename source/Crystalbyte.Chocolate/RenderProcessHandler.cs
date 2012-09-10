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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.Projections.Internal;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class RenderProcessHandler : RefCountedNativeObject {
        private readonly OnBrowserCreatedCallback _browserCreatedCallback;
        private readonly OnBrowserDestroyedCallback _browserDestroyedCallback;
        private readonly OnContextCreatedCallback _contextCreatedCallback;
        private readonly OnContextReleasedCallback _contextReleasedCallback;
        private readonly AppDelegate _delegate;
        private readonly OnProcessMessageReceivedCallback _processMessageReceivedCallback;
        private readonly OnRenderThreadCreatedCallback _renderThreadCreatedCallback;
        private readonly OnWebKitInitializedCallback _webkitInitializedCallback;

        public RenderProcessHandler(AppDelegate @delegate)
            : base(typeof (CefRenderProcessHandler)) {
            _delegate = @delegate;
            _contextCreatedCallback = OnContextCreated;
            _browserCreatedCallback = OnBrowserCreated;
            _browserDestroyedCallback = OnBrowserDestroyed;
            _renderThreadCreatedCallback = OnRenderThreadCreated;
            _webkitInitializedCallback = OnWebKitInitialized;
            _contextReleasedCallback = OnContextReleased;
            _processMessageReceivedCallback = OnProcessMessageReceived;

            MarshalToNative(new CefRenderProcessHandler {
                Base = DedicatedBase,
                OnBrowserDestroyed = Marshal.GetFunctionPointerForDelegate(_browserDestroyedCallback),
                OnContextCreated = Marshal.GetFunctionPointerForDelegate(_contextCreatedCallback),
                OnRenderThreadCreated = Marshal.GetFunctionPointerForDelegate(_renderThreadCreatedCallback),
                OnWebKitInitialized = Marshal.GetFunctionPointerForDelegate(_webkitInitializedCallback),
                OnBrowserCreated = Marshal.GetFunctionPointerForDelegate(_browserCreatedCallback),
                OnContextReleased = Marshal.GetFunctionPointerForDelegate(_contextReleasedCallback),
                OnProcessMessageReceived = Marshal.GetFunctionPointerForDelegate(_processMessageReceivedCallback)
            });
        }

        private int OnProcessMessageReceived(IntPtr self, IntPtr browser, CefProcessId sourceprocess, IntPtr message) {
            var e = new IpcMessageReceivedEventArgs {
                SourceProcess = (ProcessType) sourceprocess,
                Browser = Browser.FromHandle(browser),
                Message = IpcMessage.FromHandle(message)
            };
            _delegate.OnIpcMessageReceived(e);
            return e.IsHandled ? 1 : 0;
        }

        private void OnContextReleased(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context) {
            var e = new ContextEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Context = ScriptingContext.FromHandle(context)
            };
            _delegate.OnScriptingContextReleased(e);
        }

        private void OnBrowserCreated(IntPtr self, IntPtr browser) {
            var e = new BrowserEventArgs {
                Browser = Browser.FromHandle(browser)
            };
            _delegate.OnBrowserCreated(e);
        }

        private void OnWebKitInitialized(IntPtr self) {
            _delegate.OnInitialized(EventArgs.Empty);
        }

        private void OnRenderThreadCreated(IntPtr self) {
            _delegate.OnRenderThreadCreated(EventArgs.Empty);
        }

        private void OnContextCreated(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context) {
            var e = new ContextEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Context = ScriptingContext.FromHandle(context)
            };
            _delegate.OnScriptingContextCreated(e);
            Debug.WriteLine("Context created.");
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);
            _delegate.OnBrowserDestroyed(e);
        }
    }
}