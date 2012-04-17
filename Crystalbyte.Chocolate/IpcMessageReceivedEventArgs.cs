#region Namespace Directives

using System;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class IpcMessageReceivedEventArgs : EventArgs {
        public ProcessType SourceProcess { get; internal set; }
        public Browser Browser { get; internal set; }
        public IpcMessage Message { get; internal set; }
        public bool IsHandled { get; set; }
    }
}