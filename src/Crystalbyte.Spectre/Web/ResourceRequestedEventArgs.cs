#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class RequestProcessingEventArgs : EventArgs{
        public bool IsCanceled { get; set; }
        public Request Request { get; internal set; }
        public AsyncActivityController Controller { get; internal set; }
    }
}