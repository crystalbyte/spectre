#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class PageLoadedEventArgs : EventArgs {
        internal PageLoadedEventArgs() {}
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public int HttpStatusCode { get; internal set; }
    }
}