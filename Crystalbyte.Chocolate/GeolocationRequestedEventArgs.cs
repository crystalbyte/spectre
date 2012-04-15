using System;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate {
    public sealed class GeolocationRequestedEventArgs : EventArgs {
        internal GeolocationRequestedEventArgs() { }
        public Browser Browser { get; internal set; }
        public GeolocationRequest Request { get; internal set; }
        public string RequestingUrl { get; internal set; }
        public int RequestId { get; internal set; }
    }
}