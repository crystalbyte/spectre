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

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class IncAsyncScriptingCommand : ScriptingCommand {
        private readonly List<ContextBoundTask> _tasks;
        private readonly object _mutex;

        public IncAsyncScriptingCommand() {
            _mutex = new object();
            _tasks = new List<ContextBoundTask>();
        }

        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "incrementAsync", "callback", "value"); }
        }

        private ContextBoundTask GetTask(ScriptingContext context) {
            var any = _tasks.Any(x => x.Matches(context));
            if (!any) {
                _tasks.Add(new ContextBoundTask(context));
            }
            return _tasks.First(x => x.Matches(context));
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            // Pull arguments, this must occur on the main thread.
            var context = ScriptingContext.Current;
            var callback = e.Arguments.ElementAt(0).ToFunction();
            var value = e.Arguments.ElementAt(1).ToInteger();

            // This instance will keep track of all used resources, v8 will want them back later.
            var manager = GetTask(context);

            var task = Task.Factory.StartNew(() => {
                // Keep strong references to all arguments we're about to use.
                // In this example a scoped reference will do nicely.
                var f = callback;
                var c = context;
                var t = manager.Source.Token;

                for (var i = value + 1; i <= value + 100; i++) {
                    if (t.IsCancellationRequested) {
                        break;
                    }
                    InvokeCallback(f, c, i);
                    Thread.Sleep(15);
                }

                // Dispose all strong references after we're done using them.
                f.Dispose();
                c.Dispose();
            }, manager.Source.Token).ContinueWith(x => {
                var m = manager;
                lock (_mutex) {
                    m.Tasks.Remove(x);
                }
            });

            // Store the task object, we will need it later;
            lock (_mutex) {
                manager.Tasks.Add(task);
            }

            e.IsHandled = true;
        }

        private static void InvokeCallback(IFunction callback, ScriptingContext context, int value) {
            Application.Current.Dispatcher.InvokeAsync(() => {
                var success = context.TryEnter();
                if (!success) {
                    return;
                }
                callback.Execute(new JavaScriptObject(value));
                context.Exit();
            });
        }

        protected override void OnScriptingContextReleased(ContextEventArgs e) {
            // Grab our state object.
            var task = GetTask(e.Context);

            // Cancel all running tasks associated with the current context.
            task.Source.Cancel(false);

            // Block the thread until all tasks have ended and released their resources.
            task.Wait();

            // Release all remaining references stored inside our state object, if any.
            task.Dispose();

            // Make the managed husk eligible for garbage collection.
            _tasks.Remove(task);
        }

        private sealed class ContextBoundTask {
            private readonly ScriptingContext _context;
            private readonly List<Task> _tasks;
            private readonly CancellationTokenSource _source;

            public ContextBoundTask(ScriptingContext context) {
                _context = context;
                _tasks = new List<Task>();
                _source = new CancellationTokenSource();
            }

            public CancellationTokenSource Source {
                get { return _source; }
            }

            public List<Task> Tasks {
                get { return _tasks; }
            }

            public void Dispose() {
                if (_context != null) {
                    _context.Dispose();
                }
            }

            public void Wait() {
                Task.WaitAll(Tasks.ToArray());
            }

            public bool Matches(ScriptingContext context) {
                return context.IsSame(_context);
            }
        }
    }
}
