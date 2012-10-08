using System;

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenuOpeningEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ContextMenuArgs Arguments { get; internal set; }
    }
}