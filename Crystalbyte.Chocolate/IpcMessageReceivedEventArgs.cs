using System;
using System.IO;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate {
    public sealed class IpcMessageReceivedEventArgs : EventArgs {
        public ProcessType SourceProcess { get; internal set; }
        public Browser Browser { get; internal set; }
        public IpcMessage Message { get; internal set; }
        public bool IsHandled { get; set; }
    }
}