#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    internal class IpcBrowserDelegate : BrowserDelegate {
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
