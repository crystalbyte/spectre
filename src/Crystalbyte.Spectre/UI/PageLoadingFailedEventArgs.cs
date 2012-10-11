#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class PageLoadingFailedEventArgs : EventArgs{
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ErrorCode ErrorCode { get; internal set; }
        public string Message { get; internal set; }
        public string FailedUrl { get; internal set; }
    }
}