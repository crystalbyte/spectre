using System;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate
{
    public static class TaskManager {
        public static void InvokeAsync(Task task, DispatcherQueue queue) {
            CefTaskCapi.CefPostTask((CefThreadId) queue, task.NativeHandle);
        }

        public static void InvokeAsync(Task task, DispatcherQueue queue, TimeSpan delay){
            CefTaskCapi.CefPostDelayedTask((CefThreadId)queue, task.NativeHandle, (long) delay.TotalMilliseconds);
        }

        public static bool IsCurrentQueue(DispatcherQueue queue) {
            var result =  CefTaskCapi.CefCurrentlyOn((CefThreadId) queue);
            return Convert.ToBoolean(result);
        }
    }
}
