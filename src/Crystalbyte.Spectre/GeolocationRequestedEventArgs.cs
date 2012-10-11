#region Using directives

using System;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class GeolocationRequestedEventArgs : EventArgs{
        internal GeolocationRequestedEventArgs(){}

        public Browser Browser { get; internal set; }
        public GeolocationRequest Request { get; internal set; }
        public string RequestingUrl { get; internal set; }
        public int RequestId { get; internal set; }
    }
}