#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    public static class TaskDispatcher {
        public static void InvokeAsync(Action action, DispatcherQueue queue, TimeSpan delay) {
            var task = new Task(action);
            CefTaskCapi.CefPostDelayedTask((CefThreadId)queue, task.NativeHandle, (long)delay.TotalMilliseconds);
        }

        public static void InvokeAsync(Action action, DispatcherQueue queue) {
            var task = new Task(action);
            CefTaskCapi.CefPostTask((CefThreadId)queue, task.NativeHandle);
        }

        public static bool IsCurrentQueue(DispatcherQueue queue) {
            var result = CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}