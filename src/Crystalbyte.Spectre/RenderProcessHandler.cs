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
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class RenderProcessHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefRenderProcessHandlerCapiDelegates.OnBrowserCreatedCallback _browserCreatedCallback;
        private readonly CefRenderProcessHandlerCapiDelegates.OnBrowserDestroyedCallback _browserDestroyedCallback;
        private readonly CefRenderProcessHandlerCapiDelegates.OnContextCreatedCallback _contextCreatedCallback;
        private readonly CefRenderProcessHandlerCapiDelegates.OnContextReleasedCallback _contextReleasedCallback;
        private readonly CefRenderProcessHandlerCapiDelegates.OnRenderThreadCreatedCallback _renderThreadCreatedCallback;
        private readonly CefRenderProcessHandlerCapiDelegates.OnWebKitInitializedCallback _webkitInitializedCallback;

        private readonly CefRenderProcessHandlerCapiDelegates.OnProcessMessageReceivedCallback2
            _processMessageReceivedCallback;

        private readonly RenderDelegate _delegate;

        public RenderProcessHandler(RenderDelegate @delegate)
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
                OnBrowserDestroyed =
                    Marshal.GetFunctionPointerForDelegate(
                        _browserDestroyedCallback),
                OnContextCreated =
                    Marshal.GetFunctionPointerForDelegate(
                        _contextCreatedCallback),
                OnRenderThreadCreated =
                    Marshal.GetFunctionPointerForDelegate(
                        _renderThreadCreatedCallback),
                OnWebKitInitialized =
                    Marshal.GetFunctionPointerForDelegate(
                        _webkitInitializedCallback),
                OnBrowserCreated =
                    Marshal.GetFunctionPointerForDelegate(
                        _browserCreatedCallback),
                OnContextReleased =
                    Marshal.GetFunctionPointerForDelegate(
                        _contextReleasedCallback),
                OnProcessMessageReceived =
                    Marshal.GetFunctionPointerForDelegate(
                        _processMessageReceivedCallback)
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

            ScriptingRuntime.RegisteredExtensions.ForEach(x => x.OnScriptingContextReleased(e));
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
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);

            _delegate.OnBrowserDestroyed(e);
        }
    }
}
