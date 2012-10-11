#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class ProxyUrlEventArgs : EventArgs{
        internal ProxyUrlEventArgs(){}

        public string Url { get; internal set; }
    }
}