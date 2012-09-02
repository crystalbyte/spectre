using System;

namespace Crystalbyte.Chocolate {
    public sealed class ResourceRequestedEventArgs : EventArgs {
        public bool IsCanceled { get; set; }
        public Request Request { get; internal set; }
        public AsyncActivityController Controller { get; internal set; }
    }
}