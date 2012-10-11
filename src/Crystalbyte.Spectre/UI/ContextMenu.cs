#region Using directives

using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class ContextMenu : RefCountedNativeObject{
        public ContextMenu()
            : base(typeof (CefMenuModel)){}
    }
}