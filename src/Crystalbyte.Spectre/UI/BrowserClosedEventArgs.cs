#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class BrowserClosedEventArgs : EventArgs{
        internal BrowserClosedEventArgs(Browser browser){
            Browser = browser;
        }

        public Browser Browser { get; private set; }
    }
}