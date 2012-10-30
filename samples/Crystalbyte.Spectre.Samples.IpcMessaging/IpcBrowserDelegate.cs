using System.Diagnostics;
using System.Globalization;
using System.IO;
using Crystalbyte.Spectre.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Samples {
    class IpcBrowserDelegate : BrowserDelegate {
        protected override void OnIpcMessageReceived(IpcMessageReceivedEventArgs e) {

            using (var reader = new StreamReader(e.Message.Payload)) {
                var s = reader.ReadToEnd();
                Debug.WriteLine(s);
                e.Browser.SendIpcMessage(ProcessType.Renderer, 
                    new IpcMessage(Environment.TickCount.ToString(CultureInfo.InvariantCulture)));   
            }
            
            base.OnIpcMessageReceived(e);
        }
    }
}
