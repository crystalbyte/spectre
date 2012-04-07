#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public class ScriptingHandler : OwnedAdapter {
        private readonly V8ExecuteCallback _executeCallback;

        public ScriptingHandler()
            : base(typeof (CefV8Handler)) {
            _executeCallback = OnExecuted;
            MarshalToNative(new CefV8Handler {
                Base = DedicatedBase,
                Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
            });
        }

        private int OnExecuted(IntPtr self, IntPtr name, IntPtr obj, int argcount, IntPtr arguments, out IntPtr retvalue,
                               IntPtr exception) {
            var functionName = StringUtf16.ReadString(name);
            OnExecuted(new ExecutedEventArgs());
            retvalue = IntPtr.Zero;
            return 0;
        }

        public event EventHandler<ExecutedEventArgs> Executed;

        protected virtual void OnExecuted(ExecutedEventArgs e) {
            var handler = Executed;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}