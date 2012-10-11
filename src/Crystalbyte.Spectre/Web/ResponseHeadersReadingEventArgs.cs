#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class ResponseHeadersReadingEventArgs : EventArgs{
        public Response Response { get; internal set; }
        public Uri RedirectUri { get; set; }
    }
}