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
using System.Diagnostics;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading {
    public sealed class Dispatcher {
        private static readonly Dispatcher _dispatcher = new Dispatcher();

        private Dispatcher() {}

        public static Dispatcher Current {
            get { return _dispatcher; }
        }

        public void InvokeAsync(Action action, DispatcherQueue queue, TimeSpan delay) {
            var task = new Task(action);
            try {
                CefTaskCapi.CefPostDelayedTask((CefThreadId) queue, task.NativeHandle, (long) delay.TotalMilliseconds);
            }
            catch (AccessViolationException ex) {
                // This may occur, if someone kills the render process while posting a new task on its queue.
                Debug.WriteLine(ex);
            }
        }

        public void InvokeAsync(Action action, DispatcherQueue queue = DispatcherQueue.Renderer) {
            var task = new Task(action);
            try {
                CefTaskCapi.CefPostTask((CefThreadId) queue, task.NativeHandle);
            }
            catch (AccessViolationException ex) {
                // This may occur, if someone kills the render process while posting a new task on its queue.
                Debug.WriteLine(ex);
            }
        }

        public bool IsCurrentlyOn(DispatcherQueue queue) {
            var result = CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}
