#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public class JavaScriptHandler : OwnedRefCountedCefTypeAdapter {
        private readonly V8ExecuteCallback _executeCallback;

        private JavaScriptHandler(IntPtr handle)
            : base(typeof (CefV8handler)) {
            Handle = handle;
        }

        public JavaScriptHandler()
            : base(typeof (CefV8handler)) {
            _executeCallback = OnExecuted;
            MarshalToNative(new CefV8handler {
                Base = DedicatedBase,
                Execute = Marshal.GetFunctionPointerForDelegate(_executeCallback)
            });
        }

        public static JavaScriptHandler FromHandle(IntPtr handle) {
            return new JavaScriptHandler(handle);
        }

        private int OnExecuted(IntPtr self, IntPtr name, IntPtr obj, int argcount, IntPtr arguments, out IntPtr retvalue,
                               IntPtr exception) {
            var e = new ExecutedEventArgs {
                Arguments = new JavaScriptObjectCollection(arguments, argcount),
                FunctionName = StringUtf16.ReadString(name),
                Target = JavaScriptObject.FromHandle(obj)
            };
            var message = StringUtf16.ReadString(exception);
            if (string.IsNullOrWhiteSpace(message)) {
                e.Exception = new ScriptingException(message);
            }
            OnExecuted(e);
            if (e.Result != null) {
                Reference.Increment(e.Result);
                retvalue = e.Result.Handle;
            }
            else {
                retvalue = IntPtr.Zero;
            }
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
