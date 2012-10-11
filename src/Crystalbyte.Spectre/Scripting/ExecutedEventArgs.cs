#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Scripting{
    public sealed class ExecutedEventArgs : EventArgs{
        public string FunctionName { get; internal set; }
        public IReadOnlyCollection<JavaScriptObject> Arguments { get; internal set; }
        public JavaScriptObject Result { get; set; }
        public JavaScriptObject Target { get; internal set; }
        public bool IsHandled { get; set; }
        public Exception Exception { get; set; }
    }
}