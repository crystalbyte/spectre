#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class ScriptingContext : Adapter {
        private ScriptingContext(IntPtr handle)
            : base(typeof (CefV8context), true) {
            NativeHandle = handle;
        }

        internal static ScriptingContext FromHandle(IntPtr handle) {
            return new ScriptingContext(handle);
        }

        public static ScriptingContext Current {
            get {
                var handle = CefV8Capi.CefV8contextGetCurrentContext();
                return FromHandle(handle);
            }
        }

        public static ScriptingContext Active {
            get {
                var handle = CefV8Capi.CefV8contextGetEnteredContext();
                return FromHandle(handle);
            }
        }

        public bool TryEnter() {
            var reflection = MarshalFromNative<CefV8context>();
            var function = (EnterCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Enter, typeof(EnterCallback));
            var value = function(NativeHandle);
            return Convert.ToBoolean(value);
        }

        public void Enter() {
            var success = TryEnter();
            if (!success) {
                throw new ChocolateException("Failed to enter context.");
            }
        }

        public void Exit() {
            var success = TryExit();
            if (!success) {
                throw new ChocolateException("Failed to exit context.");
            }
        }

        public bool TryExit() {
            var reflection = MarshalFromNative<CefV8context>();
            var function = (ExitCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Exit, typeof(ExitCallback));
            var value = function(NativeHandle);
            return Convert.ToBoolean(value);
        }

        public Browser Browser {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetBrowserCallback) 
                    Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser, typeof(GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        private delegate IntPtr GetContextFrameCallback(IntPtr self);
        public Frame Frame {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetContextFrameCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetFrame, typeof(GetContextFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public ScriptableObject Document {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetGlobalCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetGlobal, typeof(GetGlobalCallback));
                var handle = function(NativeHandle);
                return ScriptableObject.FromHandle(handle);
            }
        }

        public bool IsSame(ScriptingContext other) { 
            var reflection = MarshalFromNative<CefV8context>();
            var function = (IsSameCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.IsSame, typeof(IsSameCallback));
            var value = function(NativeHandle, other.NativeHandle);
            return Convert.ToBoolean(value);
        }

        public bool Evaluate(string code, out ScriptableObject result, out ScriptingError exception) {
            result = new ScriptableObject();
            exception = new ScriptingError();
            var str = new StringUtf16(code);

            var reflection = MarshalFromNative<CefV8context>();
            var function = (EvalCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Eval, typeof(EvalCallback));
            var success = function(NativeHandle, str.NativeHandle, result.NativeHandle, exception.NativeHandle);
            return Convert.ToBoolean(success);
        }
    }
}