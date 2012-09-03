using System;
namespace Crystalbyte.Chocolate {
    public sealed class ResponseHeadersRequestedEventArgs : EventArgs {
        public Response Response { get; internal set; }
        public Uri RedirectUri { get; set; }
    }
}