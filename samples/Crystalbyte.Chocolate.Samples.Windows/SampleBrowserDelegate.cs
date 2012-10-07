using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate {
    public sealed class SampleBrowserDelegate : BrowserDelegate {

        protected override void OnJavaScriptDialogOpening(JavaScriptDialogOpeningEventArgs e) {
            base.OnJavaScriptDialogOpening(e);
        }
    }
}
