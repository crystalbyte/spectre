using System;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.UI {
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