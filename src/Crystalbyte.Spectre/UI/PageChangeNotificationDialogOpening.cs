using System;
using Crystalbyte.Spectre.Scripting;

namespace Crystalbyte.Spectre.UI {
    public sealed class PageChangeNotificationDialogOpeningEventArgs : EventArgs {
        internal PageChangeNotificationDialogOpeningEventArgs() {
            
        }
        public Browser Browser { get; internal set; }
        public string Message { get; internal set; }
        public bool IsReload { get; internal set; }
        public JavaScriptDialogCallback Callback { get; internal set; }
        public bool IsHandled { get; set; }
    }
}