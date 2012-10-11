#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class PageLoadingEventArgs : EventArgs{
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
    }
}