using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class JavaScriptDialogCallback : NativeObject {
        private readonly ContinueCallback _continueCallback;

        private delegate void ContinueCallback(IntPtr self, int success, IntPtr userInput);

        private JavaScriptDialogCallback(IntPtr handle)
            : base(typeof(CefJsdialogCallback)) {
            NativeHandle = handle;
        }

        public void Resume(bool success, string message = "") {
            var r = MarshalFromNative<CefJsdialogCallback>();
            var action = (ContinueCallback)Marshal.GetDelegateForFunctionPointer(r.Cont, typeof(ContinueCallback));
            var input = new StringUtf16(message);
            action(NativeHandle, Convert.ToInt32(success), input.NativeHandle);
            input.Free();
        }

        public static JavaScriptDialogCallback FromHandle(IntPtr handle) {
            return new JavaScriptDialogCallback(handle);
        }
    }
}
