using Crystalbyte.Spectre.Projections;

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenu : NativeObject {
        public ContextMenu() 
            : base(typeof(CefMenuModel), true) {
            
        }
    }
}
