#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class BrowserClosingEventArgs : EventArgs {
        internal BrowserClosingEventArgs(Browser browser) {
            Browser = browser;
        }

        public Browser Browser { get; internal set; }
        public bool IsCanceled { get; set; }
    }
}