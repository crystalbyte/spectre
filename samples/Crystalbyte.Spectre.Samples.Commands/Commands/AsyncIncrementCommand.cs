#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.Threading;

#endregion

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class AsyncIncrementCommand : ScriptingCommand {
        private IFunction _callback;
        private IFunction _end;
        private ScriptingContext _context;

        public override string RegistrationCode {
            get {
                return RegistrationCodes.Synthesize("commands", "incrementAsync", "begin", "callback", "end", "value");
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            // Call the begin method, we do not need to invoke, for we are on the main thread.
            // In this example the function disables the "+100" button.
            var begin = e.Arguments.ElementAt(0).ToFunction();
            begin.Execute();

            var value = e.Arguments.ElementAt(3).ToInteger();

            // Keep strong references for all objects that need to be kept alive after leaving local scope.
            _context = ScriptingContext.Current;
            _callback = e.Arguments.ElementAt(1).ToFunction();
            _end = e.Arguments.ElementAt(2).ToFunction();

            // Start new thread
            Task.Factory.StartNew(() => {
                // Count to value + 100 and call ExecuteCallback on each iteration.
                Enumerable.Range(value + 1, 100) 
                    .ForEach(x => {
                        // As most UI engines, such as WPF, Swing, Qt or Winforms, the WebKit layout engine is thread affine,
                        // being modifyable only from the rendering (NOT UI) thread. We need to invoke.
                        Dispatcher.Current.InvokeAsync(
                            () => ExecuteCallback(x));
                        Thread.Sleep(15);
                    });

                // Call the end function, which will enable the "+100" button.
                Dispatcher.Current.InvokeAsync(
                    () => {
                        try {
                            // All code executed after entering a context is compiled and run within this context.
                            // This action is mandatory in order to run the code inside the correct window/frame.
                            var success = _context.TryEnter();
                            if (!success) {
                                // May occur if the corresponding frame has changed or the window has been closed.
                                return;
                            }

                            // Execute our callback method.
                            _end.Execute();

                            // Restore the previously used context.
                            _context.TryExit();    
                        }
                        finally {
                            // Since we kept strong references, 
                            // we need to dispose of them manually after we finished using them.
                            // This won't be necessary if the object holding the references becomes disposed or goes out of scope.    
                            Cleanup();
                        }
                    });
            });

            e.IsHandled = true;
        }

        private void Cleanup() {
            if (_context != null) {
                _context.Dispose();
            }
            if (_end != null) {
                _end.Dispose();
            }
            if (_callback != null) {
                _callback.Dispose();
            }
        }

        private void ExecuteCallback(int value) {
            // All code executed after entering a context is compiled and run within this context.
            // This action is mandatory in order to run the code inside the correct window/frame.
            var success = _context.TryEnter();
            if (!success) {
                // May occur if the corresponding frame has changed or the window has been closed.
                return;
            }

            // Execute our callback method.
            _callback.Execute(arguments: new JavaScriptObject(value));

            // Restore the previously used context.
            // May occur if the corresponding frame has changed or the window has been closed.
            _context.TryExit();
        }
    }
}
