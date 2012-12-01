#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.Samples.Commands {
    public sealed class IncScriptingCommandAsync : ScriptingCommand {
        public override string RegistrationCode {
            get { return RegistrationCodes.Synthesize("commands", "incrementAsync", "callback", "value"); }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var worker = new Worker(this, e);
            worker.Run();
            e.IsHandled = true;
        }

        private sealed class Worker {
            private CancellationTokenSource CancellationTokenSource { get; set; }
            private ScriptingContext EntryContext { get; set; }
            private IFunction Callback { get; set; }
            private int Value { get; set; }
            private Task Current { get; set; }
            private ScriptingCommand Command { get; set; }

            public Worker(ScriptingCommand c, ExecutedEventArgs e) {
                // Keep this worker alive after leaving local scope using a strong reference.
                Workers.Register(this);

                CancellationTokenSource = new CancellationTokenSource();

                // Store the current context;
                EntryContext = ScriptingContext.Current;

                // Keep strong references to the passed arguments.
                Callback = e.Arguments.ElementAt(0).ToFunction();
                Value = e.Arguments.ElementAt(1).ToInteger();

                // Keep a references to the command, so we'll be able to detach the event handler later.
                Command = c;
                Command.ScriptingContextReleased += OnScriptingContextReleased;
            }

            private void OnScriptingContextReleased(object sender, ContextEventArgs e) {
                if (e.Context.IsSame(EntryContext)) {
                    Cancel();
                }
            }

            private void Cancel() {
                // Cancel the running task gracefully and wait for its completion.
                CancellationTokenSource.Cancel();
                try {
                    Current.Wait(CancellationTokenSource.Token);
                }
                catch (OperationCanceledException) {
                    Debug.WriteLine("Task has been canceled by user.", "Info");
                }
                finally {
                    // Dispose all Javascript objects, V8 needs them back ;)
                    Finish();
                }
            }

            private void Finish() {
                Command.ScriptingContextReleased -= OnScriptingContextReleased;
                if (Callback != null) {
                    Callback.Dispose();
                }
                if (EntryContext != null) {
                    EntryContext.Dispose();
                }

                // Make this worker eligible for garbage collection.
                Workers.Remove(this);
            }

            public void Run() {
                var value = Value;
                var token = CancellationTokenSource.Token;

                Current = Task.Factory.StartNew(() => {
                    token.ThrowIfCancellationRequested();
                    for (var i = value + 1; i <= value + 100; i++) {
                        token.ThrowIfCancellationRequested();
                        InvokeCallback(i);
                        Thread.Sleep(15);
                    }
                    Finish();
                }, CancellationTokenSource.Token);
            }

            private void InvokeCallback(int value) {
                Application.Current.Dispatcher.InvokeAsync(() => {
                    var success = EntryContext.TryEnter();
                    if (!success) {
                        return;
                    }
                    Callback.Execute(new JavaScriptObject(value));
                    EntryContext.Exit();
                });
            }
        }

        private static class Workers {
            private static readonly HashSet<Worker> _workers = new HashSet<Worker>();

            public static void Register(Worker worker) {
                lock (_workers) {
                    _workers.Add(worker);
                }
            }

            public static void Remove(Worker worker) {
                lock (_workers) {
                    _workers.Remove(worker);
                }
            }
        }
    }
}
