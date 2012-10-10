using System.Linq;
using Crystalbyte.Spectre.Scripting;

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class IncrementCommand : ScriptingCommand {
        public override string PrototypeCode {
            get {
                return "if(!spectre) {" +
                       "    var spectre = { };" +
                       "}" +
                       "spectre.increment = function(value) {" +
                       "    native function __increment();" +
                       "    return __increment(value);" +
                       "}";

            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            if (e.Arguments.Count < 1) {
                e.Result = new JavaScriptObject(1);
                e.IsHandled = true;
            } else {
                var b = e.Arguments.First().ToInteger();
                e.Result = new JavaScriptObject(b + 1);
            }

            e.IsHandled = true;
        }
    }
}
