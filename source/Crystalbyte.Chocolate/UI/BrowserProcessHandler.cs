using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class BrowserProcessHandler : RefCountedNativeObject {
        private readonly AppDelegate _appDelegate;

        public BrowserProcessHandler(AppDelegate appDelegate) 
            : base(typeof(CefBrowserProcessHandler)) {
            _appDelegate = appDelegate;
            // TODO: Implement event handlers.
            MarshalToNative(new CefRenderProcessHandler {
                Base = DedicatedBase
            });
        }
    }
}
