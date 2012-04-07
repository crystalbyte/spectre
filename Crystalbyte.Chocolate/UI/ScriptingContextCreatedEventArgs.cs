using System;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ScriptingContextCreatedEventArgs : EventArgs {
        internal ScriptingContextCreatedEventArgs() {
            
        }
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ScriptingContext Context { get; internal set; }
    }
}