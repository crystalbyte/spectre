#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class ProcessStartedEventArgs : EventArgs {
        internal ProcessStartedEventArgs() {}

        public string ProcessType { get; internal set; }
        public CommandLine CommandLine { get; internal set; }
    }
}