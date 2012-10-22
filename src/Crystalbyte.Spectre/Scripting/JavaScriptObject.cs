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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    [DebuggerDisplay("Value = {ToString()}")]
    public sealed class JavaScriptObject : RefCountedNativeObject, IEnumerable<KeyValuePair<string, JavaScriptObject>>,
                                           IFunction {
        private JavaScriptObject(IntPtr handle)
            : base(typeof (CefV8value)) {
            NativeHandle = handle;
        }

        protected override void DisposeNative() {
            base.DisposeNative();
        }

        public JavaScriptObject(bool value)
            : base(typeof (CefV8value)) {
            var i = Convert.ToInt32(value);
            NativeHandle = CefV8Capi.CefV8valueCreateBool(i);
        }

        public JavaScriptObject(int value)
            : base(typeof (CefV8value)) {
            NativeHandle = CefV8Capi.CefV8valueCreateInt(value);
        }

        public JavaScriptObject(double value)
            : base(typeof (CefV8value)) {
            NativeHandle = CefV8Capi.CefV8valueCreateDouble(value);
        }

        public JavaScriptObject(string value)
            : base(typeof (CefV8value)) {
            var s = new StringUtf16(value);
            NativeHandle = CefV8Capi.CefV8valueCreateString(s.NativeHandle);
            s.Free();
        }

        public JavaScriptObject(DateTime time)
            : base(typeof (CefV8value)) {
            var t = new Time(time);
            NativeHandle = CefV8Capi.CefV8valueCreateDate(t.NativeHandle);
        }

        public JavaScriptObject(string name, JavaScriptHandler handler)
            : base(typeof (CefV8value)) {
            var s = new StringUtf16(name);
            NativeHandle = CefV8Capi.CefV8valueCreateFunction(s.NativeHandle, handler.NativeHandle);
            s.Free();
        }

        public static JavaScriptObject Null {
            get {
                var handle = CefV8Capi.CefV8valueCreateNull();
                return new JavaScriptObject(handle);
            }
        }

        public static JavaScriptObject Undefined {
            get {
                var handle = CefV8Capi.CefV8valueCreateUndefined();
                return new JavaScriptObject(handle);
            }
        }

        public JavaScriptObject this[int index] {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (GetValueByindexCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetValueByindex,
                                                                     typeof (GetValueByindexCallback));
                var handle = function(NativeHandle, index);
                return FromHandle(handle);
            }
        }

        public IValueCollection<string> Keys {
            get {
                if (!IsArray) {
                    return null;
                }
                var c = new StringUtf16Collection();
                var r = MarshalFromNative<CefV8value>();
                var function = (GetKeysCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetKeys, typeof (GetKeysCallback));
                function(NativeHandle, c.NativeHandle);
                return c;
            }
        }

        public bool IsNull {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsNullCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsNull, typeof (IsNullCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsUndefined {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsUndefinedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsUndefined,
                                                                     typeof (IsUndefinedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsBoolean {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsBoolCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsBool, typeof (IsBoolCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsString {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsStringCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsString, typeof (IsStringCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsInteger {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsIntCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsInt, typeof (IsIntCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDouble {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsDoubleCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsDouble, typeof (IsDoubleCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsArray {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsArrayCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsArray, typeof (IsArrayCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDate {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsDateCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsDate, typeof (IsDateCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsObject {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsObjectCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsObject, typeof (IsObjectCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsFunction {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (IsFunctionCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsFunction, typeof (IsFunctionCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public int Length {
            get {
                if (!IsArray) {
                    return 0;
                }
                var r = MarshalFromNative<CefV8value>();
                var function = (GetArrayLengthCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetArrayLength,
                                                                     typeof (GetArrayLengthCallback));
                return function(NativeHandle);
            }
        }

        public JavaScriptObject this[string name] {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (GetValueBykeyCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetValueBykey,
                                                                     typeof (GetValueBykeyCallback));
                var s = new StringUtf16(name);
                var handle = function(NativeHandle, s.NativeHandle);
                s.Free();
                return FromHandle(handle);
            }
            set {
                var r = MarshalFromNative<CefV8value>();
                var action = (SetValueBykeyCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetValueBykey,
                                                                   typeof (SetValueBykeyCallback));
                var s = new StringUtf16(name);

                Reference.Increment(value);
                action(NativeHandle, s.NativeHandle, value.NativeHandle, CefV8Propertyattribute.V8PropertyAttributeNone);
                s.Free();
            }
        }

        public int Count {
            get { return Length; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        #region IEnumerable<KeyValuePair<string,JavaScriptObject>> Members

        public IEnumerator<KeyValuePair<string, JavaScriptObject>> GetEnumerator() {
            return new ScriptableObjectEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region IFunction Members

        string IFunction.Name {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (GetFunctionNameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFunctionName, typeof (GetFunctionNameCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        JavaScriptHandler IFunction.Handler {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (GetFunctionHandlerCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFunctionHandler,
                                                                     typeof (GetFunctionHandlerCallback));
                var handle = function(NativeHandle);
                return JavaScriptHandler.FromHandle(handle);
            }
        }

        JavaScriptObject IFunction.Execute(params JavaScriptObject[] arguments) {
            return (this as IFunction).Execute(Null, arguments);
        }

        JavaScriptObject IFunction.Execute(JavaScriptObject target, params JavaScriptObject[] arguments) {
            if (target == null)
                throw new ArgumentNullException("target");

            var r = MarshalFromNative<CefV8value>();
            var function = (ExecuteFunctionCallback)
                           Marshal.GetDelegateForFunctionPointer(r.ExecuteFunction, typeof (ExecuteFunctionCallback));
            var handle = arguments.ToUnmanagedArray();

            Reference.Increment(target.NativeHandle);
            arguments.ForEach(Reference.Increment);

            var resultHandle = function(NativeHandle, target.NativeHandle, arguments.Length, handle);

            var result = FromHandle(resultHandle);
            return result;
        }

        JavaScriptObject IFunction.Execute(ScriptingContext context, JavaScriptObject target,
                                           params JavaScriptObject[] arguments) {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.IsDisposed) {
                return null;
            }

            var r = MarshalFromNative<CefV8value>();
            var function = (ExecuteFunctionWithContextCallback)
                           Marshal.GetDelegateForFunctionPointer(r.ExecuteFunctionWithContext,
                                                                 typeof (ExecuteFunctionWithContextCallback));
            var handle = arguments.ToUnmanagedArray();

            Reference.Increment(context.NativeHandle);
            Reference.Increment(target.NativeHandle);
            arguments.ForEach(Reference.Increment);

            var resultHandle = function(NativeHandle, context.NativeHandle, target.NativeHandle, arguments.Length,
                                        handle);

            var result = FromHandle(resultHandle);
            return result;
        }

        #endregion

        public bool ContainsKey(string key) {
            var r = MarshalFromNative<CefV8value>();
            var function = (HasValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(r.HasValueBykey,
                                                                 typeof (HasValueBykeyCallback));
            var s = new StringUtf16(key);
            var contains = function(NativeHandle, s.NativeHandle);
            s.Free();
            return Convert.ToBoolean(contains);
        }

        public void Add(string key, JavaScriptObject value) {
            var r = MarshalFromNative<CefV8value>();
            var action = (SetValueBykeyCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SetValueBykey,
                                                               typeof (SetValueBykeyCallback));
            var s = new StringUtf16(key);
            Reference.Increment(value);
            action(NativeHandle, s.NativeHandle, value.NativeHandle, CefV8Propertyattribute.V8PropertyAttributeNone);
            s.Free();
        }

        public bool Remove(string key) {
            var r = MarshalFromNative<CefV8value>();
            var function = (DeleteValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(r.DeleteValueBykey,
                                                                 typeof (DeleteValueBykeyCallback));
            var s = new StringUtf16(key);
            var success = function(NativeHandle, s.NativeHandle);
            s.Free();
            return Convert.ToBoolean(success);
        }

        public bool TryGetValue(string key, out JavaScriptObject value) {
            if (!ContainsKey(key)) {
                value = null;
                return false;
            }
            value = this[key];
            return true;
        }

        public void Add(KeyValuePair<string, JavaScriptObject> item) {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<string, JavaScriptObject> item) {
            return ContainsKey(item.Key);
        }

        public bool Remove(KeyValuePair<string, JavaScriptObject> item) {
            return Remove(item.Key);
        }

        internal static JavaScriptObject FromHandle(IntPtr handle) {
            return new JavaScriptObject(handle);
        }

        public override string ToString() {
            var r = MarshalFromNative<CefV8value>();
            var function = (GetStringValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.GetStringValue,
                                                                 typeof (GetStringValueCallback));
            var handle = function(NativeHandle);
            return StringUtf16.ReadString(handle);
        }

        public DateTime ToDateTime() {
            var r = MarshalFromNative<CefV8value>();
            var function = (GetDateValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetDateValue,
                                                                                        typeof (GetDateValueCallback));
            var handle = function(NativeHandle);
            return Time.FromHandle(handle).ToDateTime();
        }

        public bool ToBoolean() {
            var r = MarshalFromNative<CefV8value>();
            var function = (GetBoolValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetBoolValue,
                                                                                        typeof (GetBoolValueCallback));
            var b = function(NativeHandle);
            return Convert.ToBoolean(b);
        }

        public int ToInteger() {
            var r = MarshalFromNative<CefV8value>();
            var function = (GetIntValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetIntValue,
                                                                                       typeof (GetIntValueCallback));
            return function(NativeHandle);
        }

        public IFunction ToFunction() {
            return this;
        }

        public double ToDouble() {
            var r = MarshalFromNative<CefV8value>();
            var function = (GetDoubleValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.GetDoubleValue,
                                                                 typeof (GetDoubleValueCallback));
            return function(NativeHandle);
        }

        public static JavaScriptObject CreateArray(int length = 4) {
            var handle = CefV8Capi.CefV8valueCreateArray(length);
            return FromHandle(handle);
        }

        #region Nested type: ScriptableObjectEnumerator

        public sealed class ScriptableObjectEnumerator : IEnumerator<KeyValuePair<string, JavaScriptObject>> {
            private readonly int _count;
            private readonly JavaScriptObject _so;
            private int _index;

            public ScriptableObjectEnumerator(JavaScriptObject so) {
                _count = so.Keys.Count;
                _so = so;
                _index = -1;
            }

            #region IEnumerator<KeyValuePair<string,JavaScriptObject>> Members

            public void Dispose() {
                // nada
            }

            public bool MoveNext() {
                _index++;
                return _index < _count;
            }

            public void Reset() {
                _index = -1;
            }

            public KeyValuePair<string, JavaScriptObject> Current {
                get {
                    var key = _so.Keys[_index];
                    var value = _so[key];
                    return new KeyValuePair<string, JavaScriptObject>(key, value);
                }
            }

            object IEnumerator.Current {
                get { return Current; }
            }

            #endregion
        }

        #endregion
                                           }
}
