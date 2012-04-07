#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class RenderProcessHandler : OwnedAdapter {
        private readonly OnBrowserDestroyedCallback _browserDestroyedCallback;
        private readonly OnContextCreatedCallback _contextCreatedCallback;
        private readonly AppDelegate _delegate;

        public RenderProcessHandler(AppDelegate @delegate)
            : base(typeof (CefRenderProcessHandler)) {
            _delegate = @delegate;
            _contextCreatedCallback = OnContextCreated;
            _browserDestroyedCallback = OnBrowserDestroyed;
            MarshalToNative(new CefRenderProcessHandler {
                Base = DedicatedBase,
                OnBrowserDestroyed = Marshal.GetFunctionPointerForDelegate(_browserDestroyedCallback),
                OnContextCreated = Marshal.GetFunctionPointerForDelegate(_contextCreatedCallback)
            });
        }

        private void OnContextCreated(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context) {
            var e = new ScriptingContextCreatedEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Context = ScriptingContext.FromHandle(context)
            };
            _delegate.OnContextCreated(e);
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);
            _delegate.OnBrowserDestroyed(e);
        }
    }
}