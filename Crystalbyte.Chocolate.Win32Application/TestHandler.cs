#region Namespace Directives

using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class TestHandler : ScriptingExtension {
        public override string PrototypeCode {
            get { return "a = function() {  native function showMessageBox(); };"; }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            MessageBox.Show("executed");
            base.OnExecuted(e);
        }
    }
}