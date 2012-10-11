#region Using directives

using System;
using Crystalbyte.Spectre.Interop;

#endregion

namespace Crystalbyte.Spectre.UI{
    internal sealed class BrowserCreationArgs{
        public Uri StartUri { get; set; }
        public ClientHandler ClientHandler { get; set; }
        public NativeObject WindowInfo { get; set; }
        public BrowserSettings Settings { get; set; }
    }
}