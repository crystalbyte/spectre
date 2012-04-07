﻿#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class CommandLineEventArgs : EventArgs {
        internal CommandLineEventArgs() {}

        public string ProcessType { get; internal set; }
        public CommandLine CommandLine { get; internal set; }
    }
}