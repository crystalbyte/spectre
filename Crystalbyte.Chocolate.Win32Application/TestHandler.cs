#region Namespace Directives

using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class TestHandler : ScriptingExtension {
        public override string PrototypeCode {
            get { return "native function sum(a, b);"; }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var x = e.Arguments[0].ToInteger();
            var y = e.Arguments[1].ToInteger();
            e.IsHandled = true;
            e.Result = new ScriptableObject(x + y);
            base.OnExecuted(e);
        }
    }
}