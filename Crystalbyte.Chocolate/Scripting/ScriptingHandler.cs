using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Scripting
{
    public class ScriptingHandler : OwnedAdapter {
        private readonly V8ExecuteCallback _executeCallback;
        public ScriptingHandler() 
            : base(typeof(CefV8Handler)) {
            _executeCallback = OnExecuted;
            MarshalToNative(new CefV8Handler {
                Base = DedicatedBase,
                Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
            });
        }

        private int OnExecuted(IntPtr self, IntPtr name, IntPtr obj, int argcount, IntPtr arguments, out IntPtr retvalue, IntPtr exception) {
            var functionName = StringUtf16.ReadString(name);
            try {
                OnExecuted(new ExecutedEventArgs());
            }
            catch (Exception) {
                throw;
            }
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
