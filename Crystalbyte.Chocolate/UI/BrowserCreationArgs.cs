#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class BrowserCreationArgs {
        public Uri StartUri { get; set; }
        public ClientHandler ClientHandler { get; set; }
        public WindowsWindowInfo WindowInfo { get; set; }
        public BrowserSettings Settings { get; set; }
    }
}