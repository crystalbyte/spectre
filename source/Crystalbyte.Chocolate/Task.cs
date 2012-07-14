#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class Task : OwnedAdapter {
        private readonly Action _action;
        private readonly ExecuteCallback _executeCallback;

        public Task(Action action)
            : base(typeof (CefTask)) {
            _action = action;
            _executeCallback = OnExecute;
            MarshalToNative(new CefTask {
                Base = DedicatedBase,
                Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
            });
        }

        private void OnExecute(IntPtr self, CefThreadId threadid) {
            if (_action != null) {
                _action();
            }
        }
    }
}