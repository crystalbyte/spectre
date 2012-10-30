using Crystalbyte.Spectre.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Samples {
    class ContextMenuBrowserDelegate : BrowserDelegate {

        protected override void OnContextMenuOpening(ContextMenuOpeningEventArgs e) {
            e.Menu.Items.Clear();
            e.Menu.Items.Add(new LabelMenuItem("Navigate to Google") { Command = 26500 });
            base.OnContextMenuOpening(e);   
        }

        protected override void OnContextMenuCommand(ContextMenuCommandEventArgs e) {
            base.OnContextMenuCommand(e);
            if (e.Command == 26500) {
                e.Browser.MainFrame.Navigate("http://www.google.de");
            }
        }
    }
}
