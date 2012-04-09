#region Namespace Directives

using System;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class ExecutedEventArgs : EventArgs {
        public string FunctionName { get; internal set; }
        public ISealedCollection<ScriptableObject> Arguments { get; internal set; }
        public ScriptableObject Result { get; set; }
        public ScriptableObject Object { get; internal set; }
        public bool IsHandled { get; set; }
        public Exception Exception { get; internal set; }
    }
}