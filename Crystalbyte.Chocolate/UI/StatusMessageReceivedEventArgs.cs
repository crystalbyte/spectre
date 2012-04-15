using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class StatusMessageReceivedEventArgs : EventArgs {
        internal StatusMessageReceivedEventArgs() { }
        public Browser Browser { get; internal set; }
        public string Message { get; internal set; }
        public StatusType StatusType { get; internal set; }
    }
}