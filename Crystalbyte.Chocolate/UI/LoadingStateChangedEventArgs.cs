using System;

namespace Crystalbyte.Chocolate.UI {
    public sealed class LoadingStateChangedEventArgs : EventArgs {
        public Browser Browser { get; internal set; }
        public bool IsLoading { get; internal set; }
        public bool CanGoForward { get; internal set; }
        public bool CanGoBack { get; internal set; }
    }
}