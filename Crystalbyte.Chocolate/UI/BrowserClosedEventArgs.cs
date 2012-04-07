#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class BrowserClosedEventArgs : EventArgs {
        internal BrowserClosedEventArgs(Browser browser) {
            Browser = browser;
        }

        public Browser Browser { get; private set; }
    }
}