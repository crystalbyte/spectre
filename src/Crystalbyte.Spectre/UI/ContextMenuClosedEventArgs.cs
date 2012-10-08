using System;

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenuClosedEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
    }
}