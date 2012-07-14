#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class ScriptingException : Adapter {
        public ScriptingException()
            : base(typeof (CefV8exception)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
        }

        public string Message {
            get {
                var reflection = MarshalFromNative<CefV8exception>();
                var function = (GetMessageCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetMessage, typeof (GetMessageCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadString(handle);
            }
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}