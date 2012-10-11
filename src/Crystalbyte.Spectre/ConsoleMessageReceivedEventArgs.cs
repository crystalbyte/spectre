#region Using directives

using System;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class ConsoleMessageReceivedEventArgs : EventArgs{
        internal ConsoleMessageReceivedEventArgs(){}

        public bool IsSuppressed { get; set; }
        public Browser Browser { get; internal set; }
        public string Message { get; internal set; }
        public string Source { get; internal set; }
        public int Line { get; internal set; }
    }
}