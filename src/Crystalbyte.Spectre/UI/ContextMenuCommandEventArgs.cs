using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ContextMenuCommandEventArgs : EventArgs {
        public Frame Frame { get; internal set; }
        public Browser Browser { get; internal set; }
    }
}