using Crystalbyte.Spectre.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Samples {
    class SampleBrowserDelegate : BrowserDelegate {
        protected override void OnContextMenuOpening(ContextMenuOpeningEventArgs e) {
            e.Menu.Items.Clear();
            e.Menu.Items.Add(new LabelMenuItem("Close") {Command = 26500});
            base.OnContextMenuOpening(e);   
        }

        protected override void OnContextMenuCommand(ContextMenuCommandEventArgs e) {
            base.OnContextMenuCommand(e);
        }
    }
}
