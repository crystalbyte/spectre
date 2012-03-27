using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class BrowserArgs {
        public Uri StartUri { get; set; }
        public ClientHandler ClientHandler { get; set; }
        public WindowInfo WindowInfo { get; set; }
        public ViewSettings Settings { get; set; }
    }
}
