using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ContextMenuClosedEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
    }
}