#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class PageLoadedEventArgs : EventArgs{
        internal PageLoadedEventArgs(){}

        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public int HttpStatusCode { get; internal set; }
    }
}