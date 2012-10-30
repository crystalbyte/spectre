using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Samples {
    class IpcAppDelegate : AppDelegate {
        protected override void OnIpcMessageReceived(IpcMessageReceivedEventArgs e) {
            Debug.WriteLine("Received message in process {0}", Process.GetCurrentProcess().Id);
            base.OnIpcMessageReceived(e);
        }
    }
}
