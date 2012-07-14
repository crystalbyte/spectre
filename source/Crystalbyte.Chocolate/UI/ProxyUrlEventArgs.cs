#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class ProxyUrlEventArgs : EventArgs {
        internal ProxyUrlEventArgs() {}

        public string Url { get; internal set; }
    }
}