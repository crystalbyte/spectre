using System;
using Crystalbyte.Spectre.Scripting;

namespace Crystalbyte.Spectre.UI {
    public sealed class JavaScriptDialogOpeningEventArgs : EventArgs {
        internal JavaScriptDialogOpeningEventArgs() {
            
        }

        public string AcceptedLanguage { get; internal set; }
        public Browser Browser { get; internal set; }
        public string Origin { get; internal set; }
        public DialogType DialogType { get; internal set; }
        public string Message { get; internal set; }
        public string DefaultPrompt { get; internal set; }
        public bool IsCanceled { get; internal set; }
        public bool IsHandled { get; set; }
        public DialogResult Result { get; set; }
    }
}