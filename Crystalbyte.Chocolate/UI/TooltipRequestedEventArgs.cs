using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class TooltipRequestedEventArgs : EventArgs {
        internal TooltipRequestedEventArgs() { }
        public bool IsCanceled { get; set; }
        public Browser Browser { get; internal set; }
        public string Text { get; set; }
    }
}