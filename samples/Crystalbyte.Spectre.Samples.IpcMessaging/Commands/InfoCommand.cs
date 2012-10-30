using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Scripting;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;

namespace Crystalbyte.Spectre.Samples.Commands {
    class InfoCommand : ScriptingCommand {
        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "getPID"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var id = Process.GetCurrentProcess().Id;
            e.Result  = new JavaScriptObject(id);
            e.IsHandled = true;
        }
    }
}
