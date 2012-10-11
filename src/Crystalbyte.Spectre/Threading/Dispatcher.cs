#region Using directives

using System;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Threading{
    public sealed class Dispatcher{
        private static readonly Dispatcher _dispatcher = new Dispatcher();

        private Dispatcher(){}

        public static Dispatcher Current{
            get { return _dispatcher; }
        }

        public void InvokeAsync(Action action, DispatcherQueue queue, TimeSpan delay){
            var task = new Task(action);
            CefTaskCapi.CefPostDelayedTask((CefThreadId) queue, task.NativeHandle, (long) delay.TotalMilliseconds);
        }

        public void InvokeAsync(Action action, DispatcherQueue queue = DispatcherQueue.Renderer){
            var task = new Task(action);
            CefTaskCapi.CefPostTask((CefThreadId) queue, task.NativeHandle);
        }

        public bool IsCurrentlyOn(DispatcherQueue queue){
            var result = CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}