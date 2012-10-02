using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate.Extensions
{
    class PerformSyncOperationExtension : RuntimeExtension
    {
        public override string PrototypeCode {
            get {
                return "if (!chocolate) " +
                       "    var chocolate = {};" +
                       "chocolate.sync = function() {" +
                       "    native function __sync();" +
                       "    return __sync();" +
                       "}";
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {

            MessageBox.Show("Sync");
        }
    }
}
