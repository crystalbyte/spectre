using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;

namespace Crystalbyte.Chocolate
{
    public sealed class TestHandler : ScriptingExtension {
        protected override void OnExecuted(ExecutedEventArgs e){
            MessageBox.Show("executed");
            base.OnExecuted(e);
        }

        public override string PrototypeCode {
            get { return "a = function() {  native function showMessageBox(); };"; }
        }
    }
}
