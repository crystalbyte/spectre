using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class TitleChangedEventArgs : EventArgs {
        internal TitleChangedEventArgs() { }
        public Browser Browser { get; internal set; }
        public string Title { get; internal set; }
    }
}