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
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public class ScriptingHandler : RefCountedNativeObject {
        private readonly V8ExecuteCallback _executeCallback;

        public ScriptingHandler()
            : base(typeof (CefV8handler)) {
            _executeCallback = OnExecuted;
            MarshalToNative(new CefV8handler {
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

        #region Nested type: V8ExecuteCallback

        private delegate int V8ExecuteCallback(
            IntPtr self, IntPtr name, IntPtr obj, int argcount, IntPtr arguments, out IntPtr retvalue,
            IntPtr exception);

        #endregion
    }
}