#region Namespace Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class RenderProcessHandler : OwnedAdapter {
        private readonly AppDelegate _delegate;
        private readonly OnBrowserCreatedCallback _browserCreatedCallback;
        private readonly OnBrowserDestroyedCallback _browserDestroyedCallback;
        private readonly OnContextCreatedCallback _contextCreatedCallback;
        private readonly OnContextReleasedCallback _contextReleasedCallback;
        private readonly OnRenderThreadCreatedCallback _renderThreadCreatedCallback;
        private readonly OnWebKitInitializedCallback _webkitInitializedCallback;
        private readonly OnProcessMessageRecievedCallback _processMessageReceivedCallback;

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
                OnProcessMessageRecieved = Marshal.GetFunctionPointerForDelegate(_processMessageReceivedCallback)
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
            _delegate.OnContextReleased(e);
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
            _delegate.OnContextCreated(e);
            Debug.WriteLine("Context created.");
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);
            _delegate.OnBrowserDestroyed(e);
        }
    }
}