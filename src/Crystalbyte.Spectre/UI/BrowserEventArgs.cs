#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class BrowserEventArgs : EventArgs{
        internal BrowserEventArgs(Browser browser){
            Browser = browser;
        }

        internal BrowserEventArgs(){}

        public Browser Browser { get; internal set; }
    }
}