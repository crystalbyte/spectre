using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Crystalbyte.Spectre.Scripting;
using System.Threading;
using System.Threading.Tasks;
using Crystalbyte.Spectre.Threading;

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class AsyncIncrementCommand : ScriptingCommand {
        public override string PrototypeCode {
            get {
                return "if(!spectre) {" +
                       "    var spectre = { };" +
                       "}" +
                       "spectre.incrementAsync = function(value) {" +
                       "    native function __incrementAsync();" +
                       "    return __incrementAsync(value);" +
                       "}";

            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var argument = e.Arguments.FirstOrDefault();
            if (argument == null) {
                throw new IndexOutOfRangeException("Function must be passed as first argument.");
            }
            if (!argument.IsFunction) {
                throw new InvalidOperationException("First argument must be a function.");
            }

            var callback = argument.ToFunction();

            Task.Factory.StartNew(() =>
                Enumerable.Range(0, 20).ForEach(x => {
                    callback.Execute(JavaScriptObject.Null, new JavaScriptObject(x));
                    Thread.Sleep(1000);
                }));
        }
    }
}
