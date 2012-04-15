using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class BrowserLoadingEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
    }
}