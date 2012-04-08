#region Namespace Directives

using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class TestHandler : ScriptingExtension {
        public override string PrototypeCode {
            get { return "native function a();"; }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            MessageBox.Show(@"Executed from Javascript.");
            base.OnExecuted(e);
        }
    }
}