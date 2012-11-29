#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class ScriptingContext : RefCountedCefTypeAdapter, IEquatable<ScriptingContext> {
        public override int GetHashCode() {
            return Handle.ToInt32() ^ 4;
        }

        private ScriptingContext(IntPtr handle)
            : base(typeof (CefV8context)) {
            Handle = handle;
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
                var r = MarshalFromNative<CefV8context>();
                var function = (CefBrowserCapiDelegates.GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetBrowser,
                                                                     typeof (CefBrowserCapiDelegates.GetBrowserCallback));
                var handle = function(Handle);
                return Browser.FromHandle(handle);
            }
        }

        public Frame Frame {
            get {
                var r = MarshalFromNative<CefV8context>();
                var function = (GetContextFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrame,
                                                                     typeof (GetContextFrameCallback));
                var handle = function(Handle);
                return Frame.FromHandle(handle);
            }
        }

        public JavaScriptObject Document {
            get {
                var r = MarshalFromNative<CefV8context>();
                var function = (CefV8CapiDelegates.GetGlobalCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetGlobal,
                                                                     typeof (CefV8CapiDelegates.GetGlobalCallback));
                var handle = function(Handle);
                return JavaScriptObject.FromHandle(handle);
            }
        }

        internal static ScriptingContext FromHandle(IntPtr handle) {
            return new ScriptingContext(handle);
        }

        public bool TryEnter() {
            if (Handle == IntPtr.Zero || IsDisposed) {
                return false;
            }
            var r = MarshalFromNative<CefV8context>();
            var function = (CefV8CapiDelegates.EnterCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Enter, typeof (CefV8CapiDelegates.EnterCallback));
            var value = function(Handle);
            return Convert.ToBoolean(value);
        }

        public void Enter() {
            var success = TryEnter();
            if (!success) {
                throw new ScriptingException("Failed to enter context.");
            }
        }

        public void Exit() {
            var success = TryExit();
            if (!success) {
                throw new ScriptingException("Failed to exit context.");
            }
        }

        public bool TryExit() {
            if (Handle == IntPtr.Zero || IsDisposed) {
                return false;
            }
            var r = MarshalFromNative<CefV8context>();
            var function = (CefV8CapiDelegates.ExitCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Exit, typeof (CefV8CapiDelegates.ExitCallback));
            var value = function(Handle);
            return Convert.ToBoolean(value);
        }

        public bool IsSame(ScriptingContext other) {
            if (IsDisposed || other.IsDisposed) {
                return false;
            }
            var r = MarshalFromNative<CefV8context>();
            var function = (CefDomCapiDelegates.IsSameCallback)
                           Marshal.GetDelegateForFunctionPointer(r.IsSame, typeof (CefDomCapiDelegates.IsSameCallback));

            Reference.Increment(other);

            var value = function(Handle, other.Handle);
            return Convert.ToBoolean(value);
        }

        internal bool Evaluate(string code, out JavaScriptObject result, out ScriptingExceptionObject exception) {
            result = JavaScriptObject.Null;
            exception = new ScriptingExceptionObject();
            var str = new StringUtf16(code);

            var r = MarshalFromNative<CefV8context>();
            var function = (CefV8CapiDelegates.EvalCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Eval, typeof (CefV8CapiDelegates.EvalCallback));

            Reference.Increment(result.Handle);
            Reference.Increment(exception.Handle);

            var success = function(Handle, str.Handle, result.Handle, exception.Handle);
            return Convert.ToBoolean(success);
        }

        #region Nested type: GetContextFrameCallback

        private delegate IntPtr GetContextFrameCallback(IntPtr self);

        #endregion

        public override bool Equals(object obj) {
            if (obj is ScriptingContext) {
                return Equals(obj as ScriptingContext);
            }

            return false;
        }

        public bool Equals(ScriptingContext other) {
            return other.IsSame(this);
        }
    }
}
