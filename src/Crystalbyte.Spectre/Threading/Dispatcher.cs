#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
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

        public static void InvokeAsync(Action action, DispatcherQueue queue, TimeSpan delay) {
            var task = new Task(action);
            CefTaskCapi.CefPostDelayedTask((CefThreadId) queue, task.NativeHandle, (long) delay.TotalMilliseconds);
        }

        public static void InvokeAsync(Action action, DispatcherQueue queue = DispatcherQueue.UI) {
            var task = new Task(action);
            CefTaskCapi.CefPostTask((CefThreadId) queue, task.NativeHandle);
        }

        public static bool IsCurrentlyOn(DispatcherQueue queue) {
            var result = CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}