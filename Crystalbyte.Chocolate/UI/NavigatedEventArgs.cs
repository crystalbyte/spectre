using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class NavigatedEventArgs : EventArgs {
        internal NavigatedEventArgs(string address, Browser browser, Frame frame) {
            Address = address;
            Browser = browser;
            Frame = frame;
        }
        public string Address { get; private set; }
        public Frame Frame { get; internal set; }
        public Browser Browser { get; internal set; }
    }
}
