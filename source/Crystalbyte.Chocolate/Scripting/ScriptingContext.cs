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
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class ScriptingContext : NativeObject {
        private ScriptingContext(IntPtr handle)
            : base(typeof (CefV8context), true) {
            NativeHandle = handle;
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

        public Browser Browser {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser, typeof (GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public Frame Frame {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetContextFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetFrame,
                                                                     typeof (GetContextFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public ScriptableObject Document {
            get {
                var reflection = MarshalFromNative<CefV8context>();
                var function = (GetGlobalCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetGlobal, typeof (GetGlobalCallback));
                var handle = function(NativeHandle);
                return ScriptableObject.FromHandle(handle);
            }
        }

        internal static ScriptingContext FromHandle(IntPtr handle) {
            return new ScriptingContext(handle);
        }

        public bool TryEnter() {
            var reflection = MarshalFromNative<CefV8context>();
            var function = (EnterCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.Enter, typeof (EnterCallback));
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
                           Marshal.GetDelegateForFunctionPointer(reflection.Exit, typeof (ExitCallback));
            var value = function(NativeHandle);
            return Convert.ToBoolean(value);
        }

        public bool IsSame(ScriptingContext other) {
            var reflection = MarshalFromNative<CefV8context>();
            var function = (IsSameCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.IsSame, typeof (IsSameCallback));
            var value = function(NativeHandle, other.NativeHandle);
            return Convert.ToBoolean(value);
        }

        public bool Evaluate(string code, out ScriptableObject result, out ScriptingException exception) {
            result = new ScriptableObject();
            exception = new ScriptingException();
            var str = new StringUtf16(code);

            var reflection = MarshalFromNative<CefV8context>();
            var function = (EvalCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.Eval, typeof (EvalCallback));
            var success = function(NativeHandle, str.NativeHandle, result.NativeHandle, exception.NativeHandle);
            return Convert.ToBoolean(success);
        }

        #region Nested type: GetContextFrameCallback

        private delegate IntPtr GetContextFrameCallback(IntPtr self);

        #endregion
    }
}