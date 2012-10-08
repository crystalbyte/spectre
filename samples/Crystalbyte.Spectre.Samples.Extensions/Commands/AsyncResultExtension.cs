using System.Diagnostics;
using System.Linq;
using Crystalbyte.Spectre.Scripting;

namespace Crystalbyte.Spectre.Samples.Extensions {
    public sealed class AsyncResultExtension : RuntimeCommand {
        public override string PrototypeCode {
            get {
                return "if (!chocolate) " +
                    "    var chocolate = {};" +
                    "chocolate.doCallback = function(callback) {" +
                    "    native function __doCallback ();" +
                    "    return __doCallback (callback);" +
                    "}";
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            Debug.Assert(e.Arguments.Any());
            Debug.Assert(e.Arguments.First().IsFunction);
        }
    }
}
