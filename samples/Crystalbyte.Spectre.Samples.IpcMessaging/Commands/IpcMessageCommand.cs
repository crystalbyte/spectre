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
using System.IO;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples.Commands {
    internal class IpcMessageCommand : ScriptingCommand {
        private TimeSpan _time;

        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "sendMessage", "message"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            // We are inside a render process
            _time = TimeSpan.FromMilliseconds(Environment.TickCount);

            var s = e.Arguments.First().ToString();
            var bytes = Encoding.UTF8.GetBytes(s);
            Browser.Current.SendIpcMessage(ProcessType.Browser, new IpcMessage(_time.ToString()) {
                Payload = new MemoryStream(bytes)
            });
        }
    }
}
