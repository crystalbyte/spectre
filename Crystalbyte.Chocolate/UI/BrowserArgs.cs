#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class BrowserArgs {
        public Uri StartUri { get; set; }
        public ClientHandler ClientHandler { get; set; }
        public WindowInfo WindowInfo { get; set; }
        public RenderSettings Settings { get; set; }
    }
}