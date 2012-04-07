using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    public sealed class RenderProcessHandler : OwnedAdapter
    {
        private readonly ProcessDelegate _delegate;
        private readonly OnBrowserDestroyedCallback _browserDestroyedCallback;

        public RenderProcessHandler(ProcessDelegate @delegate)
            : base(typeof(CefRenderProcessHandler)) {
            _delegate = @delegate;
            _browserDestroyedCallback = OnBrowserDestroyed;
            MarshalToNative(new CefRenderProcessHandler {
                Base = DedicatedBase,
                OnBrowserDestroyed = Marshal.GetFunctionPointerForDelegate(_browserDestroyedCallback)
            });
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser) {
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);
            _delegate.OnBrowserDestroyed(e);
        }
    }
}
