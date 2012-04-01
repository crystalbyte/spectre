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
            base.OnExecuted(e);
            MessageBox.Show("executed");
        }

        public override string PrototypeCode {
            get { return "native function c();"; }
        }
    }
}
