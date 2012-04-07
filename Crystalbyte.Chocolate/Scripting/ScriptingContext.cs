using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.Scripting
{
    public sealed class ScriptingContext : Adapter
    {
        private ScriptingContext(IntPtr handle) 
            : base(typeof(CefV8context), true) {
            NativeHandle = handle;
        }

        public static ScriptingContext FromHandle(IntPtr handle) {
            return new ScriptingContext(handle);
        }
    }
}
