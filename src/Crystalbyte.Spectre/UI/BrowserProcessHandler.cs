#region Using directives

using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI{
    internal sealed class BrowserProcessHandler : OwnedRefCountedNativeObject{
        private readonly AppDelegate _appDelegate;

        public BrowserProcessHandler(AppDelegate appDelegate)
            : base(typeof (CefBrowserProcessHandler)){
            _appDelegate = appDelegate;
            // TODO: Implement event handlers.
            MarshalToNative(new CefBrowserProcessHandler{
                                                            Base = DedicatedBase
                                                        });
        }
    }
}