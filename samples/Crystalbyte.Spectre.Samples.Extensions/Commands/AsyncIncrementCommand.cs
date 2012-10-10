using System.Diagnostics;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.Threading;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class AsyncIncrementCommand : ScriptingCommand
    {
        private IFunction _callback;
        private ScriptingContext _context;

        public override string RegistrationCode {
            get {
                return "if(!commands) {" +
                       "    var commands = { };" +
                       "}" +
                       "commands.incrementAsync = function(callback, value) {" +
                       "    native function __incrementAsync();" +
                       "    return __incrementAsync(callback, value);" +
                       "}";

            }
        }

        protected override void OnExecuted(ExecutedEventArgs e)
        {
            var callbackValue = e.Arguments.ElementAtOrDefault(0);
            var startValue = e.Arguments.ElementAtOrDefault(1);

            // Keep strong references
            _callback = callbackValue.ToFunction();
            var value = startValue.ToInteger();
            _context = ScriptingContext.Current;

            // Start new thread
            Task.Factory.StartNew(() =>
                Enumerable.Range(value, 200)
                .ForEach(x => {
                    Dispatcher.Current.InvokeAsync(
                        () => ExecuteCallback(x));
                    Thread.Sleep(15);
                }));
        }

        private void ExecuteCallback(int value)
        {
            _context.Enter();
            _callback.Execute(JavaScriptObject.Null, new JavaScriptObject(value));
            _context.Exit();
        }
    }
}
