using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre {
    public sealed class ProcessLaunchingEventArgs : EventArgs {
        internal ProcessLaunchingEventArgs() { }
        public CommandLine CommandLine { get; internal set; }
    }
}
