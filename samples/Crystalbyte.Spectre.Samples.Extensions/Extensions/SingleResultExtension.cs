using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.Samples.Extensions {
    public sealed class SingleResultExtension : RuntimeExtension {
        public override string PrototypeCode {
            get {
                return "if (!chocolate) " +
                    "    var chocolate = {};" +
                    "chocolate.doCall = function(x, y) {" +
                    "    native function __doCall ();" +
                    "    return __doCall (x, y);" +
                    "}";
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            base.OnExecuted(e);
        }
    }
}
