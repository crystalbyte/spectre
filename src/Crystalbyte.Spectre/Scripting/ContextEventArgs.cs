#region Using directives

using System;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Scripting{
    public sealed class ContextEventArgs : EventArgs{
        internal ContextEventArgs(){}

        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ScriptingContext Context { get; internal set; }
    }
}