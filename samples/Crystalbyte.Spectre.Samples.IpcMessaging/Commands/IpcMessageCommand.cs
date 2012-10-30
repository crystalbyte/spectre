using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using System.Diagnostics;

namespace Crystalbyte.Spectre.Samples.Commands {
    class IpcMessageCommand : ScriptingCommand {
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
