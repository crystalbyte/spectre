#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class WindowDelegate : ProcessDelegate {
        protected override void OnNavigated(NavigatedEventArgs e){
            base.OnNavigated(e);
        }

        protected override void OnConsoleMessageReceived(ConsoleMessageReceivedEventArgs e) {
            base.OnConsoleMessageReceived(e);
        }

        protected override void OnBrowserClosing(BrowserClosingEventArgs e) {
            base.OnBrowserClosing(e);
        }

        protected override void OnBrowserClosed(BrowserClosedEventArgs e)
        {
            base.OnBrowserClosed(e);
        }
    }
}