#region Using directives

using System;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class IpcMessageReceivedEventArgs : EventArgs{
        public ProcessType SourceProcess { get; internal set; }
        public Browser Browser { get; internal set; }
        public IpcMessage Message { get; internal set; }
        public bool IsHandled { get; set; }
    }
}