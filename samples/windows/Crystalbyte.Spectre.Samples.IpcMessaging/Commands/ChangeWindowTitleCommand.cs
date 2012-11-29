using System.Linq;
using Crystalbyte.Spectre.Scripting;

namespace Crystalbyte.Spectre.Samples.Commands {
    class ChangeWindowTitleCommand : ScriptingCommand {
        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "changeWindowTitle", "title"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var title = e.Arguments.First().ToString();
            if (string.IsNullOrWhiteSpace(title)) {
                // Chromium does not allow to send nothing over the wire, so we send the termination symbol instead.
                title = "\0";
            }

            var browser = ScriptingContext.Current.Browser;
            browser.SendIpcMessage(ProcessType.Browser, new IpcMessage("change-window-title") {
                Payload = title.ToUtf8Stream()
            });

            e.IsHandled = true;
        }
    }
}
