using System.Diagnostics;
using System.Linq;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.Samples.Extensions {
    public sealed class AsyncResultExtension : RuntimeExtension {
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
