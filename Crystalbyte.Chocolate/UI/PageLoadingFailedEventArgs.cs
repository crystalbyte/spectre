#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class PageLoadingFailedEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public ErrorCode ErrorCode { get; internal set; }
        public string Message { get; internal set; }
        public string FailedUrl { get; internal set; }
    }
}