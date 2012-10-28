using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Scripting;
using System.Windows.Forms;

namespace Crystalbyte.Spectre.Samples.Commands {
    class DockingCommand : ScriptingCommand {
        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "dock", "value"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            base.OnExecuted(e);
        }
    }
}
