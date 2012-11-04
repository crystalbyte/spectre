using System.Diagnostics;
using Crystalbyte.Spectre.Scripting;

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
