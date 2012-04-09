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
            var e = new ExecutedEventArgs {
                Arguments = new ScriptableObjectCollection(arguments, argcount),
                FunctionName = StringUtf16.ReadString(name),
                Object = ScriptableObject.FromHandle(obj)
            };
            var message = StringUtf16.ReadString(exception);
            if (string.IsNullOrWhiteSpace(message)) {
                e.Exception = new ChocolateException(message);
            }
            OnExecuted(e);
            retvalue = e.Result != null ? e.Result.NativeHandle : IntPtr.Zero;
            return Convert.ToInt32(e.IsHandled);
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