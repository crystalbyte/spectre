#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class ProcessStartedEventArgs : EventArgs{
        internal ProcessStartedEventArgs(){}

        public string ProcessType { get; internal set; }
        public CommandLine CommandLine { get; internal set; }
    }
}