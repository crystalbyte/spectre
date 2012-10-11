#region Using directives

using System;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class StatusMessageReceivedEventArgs : EventArgs{
        internal StatusMessageReceivedEventArgs(){}

        public Browser Browser { get; internal set; }
        public string Message { get; internal set; }
    }
}