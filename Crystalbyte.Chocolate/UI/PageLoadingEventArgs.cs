#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class PageLoadingEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
    }
}