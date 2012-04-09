#region Namespace Directives

using System;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class ContextEventArgs : EventArgs {
        internal ContextEventArgs() {}
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ScriptingContext Context { get; internal set; }
    }
}