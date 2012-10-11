#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class ContextMenuCommandEventArgs : EventArgs{
        public Frame Frame { get; internal set; }
        public Browser Browser { get; internal set; }
    }
}