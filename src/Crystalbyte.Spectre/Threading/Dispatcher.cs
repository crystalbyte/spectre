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
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading {
    public sealed class Dispatcher {
        private static readonly Dispatcher _dispatcher = new Dispatcher();
        private readonly HashSet<Task> _tasks;
        private readonly object _mutex;

        private Dispatcher() {
            _mutex = new object();
            _tasks = new HashSet<Task>();
        }

        public static Dispatcher Current {
            get { return _dispatcher; }
        }

        public void InvokeAsync(Action action, DispatcherQueue queue, TimeSpan delay) {
            var task = new Task(action);
            CefTaskCapi.CefPostDelayedTask((CefThreadId) queue, task.Handle, (long) delay.TotalMilliseconds);
        }

        public void InvokeAsync(Action action, DispatcherQueue queue = DispatcherQueue.Renderer) {
            var task = new Task(action);
            task.Executed += OnExecuted;
            CefTaskCapi.CefPostTask((CefThreadId) queue, task.Handle);
            lock (_mutex) {
                _tasks.Add(task);
            }
        }

        private void OnExecuted(object sender, EventArgs e) {
            lock (_mutex) {
                _tasks.Remove((Task) sender);
            }
        }

        public bool IsCurrentlyOn(DispatcherQueue queue) {
            var result = CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}
