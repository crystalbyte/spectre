#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class PopupCreatingEventArgs : EventArgs {
        internal PopupCreatingEventArgs() {}
        public Browser Parent { get; internal set; }
        public WindowInfo Info { get; internal set; }
        public BrowserSettings Settings { get; internal set; }
        public string Address { get; internal set; }
        public bool IsCanceled { get; set; }
    }
}