#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class TitleChangedEventArgs : EventArgs{
        internal TitleChangedEventArgs(){}

        public Browser Browser { get; internal set; }
        public string Title { get; internal set; }
    }
}