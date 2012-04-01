#region Namespace Directives

using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class App : CountedAdapter {
        public App()
            : base(typeof (CefApp)) {
            MarshalToNative(new CefApp {
                Base = DedicatedBase
            });
        }
    }
}