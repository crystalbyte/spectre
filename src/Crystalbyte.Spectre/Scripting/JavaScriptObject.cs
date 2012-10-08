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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    [DebuggerDisplay("Value = {ToString()}")]
    public sealed class JavaScriptObject : NativeObject, IEnumerable<KeyValuePair<string, JavaScriptObject>> {
        private JavaScriptObject(IntPtr handle)
            : base(typeof (CefV8value), true) {
            NativeHandle = handle;
        }

        public JavaScriptObject()
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateNull();
        }

        public JavaScriptObject(bool value)
            : base(typeof (CefV8value), true) {
            var i = Convert.ToInt32(value);
            NativeHandle = CefV8Capi.CefV8valueCreateBool(i);
        }

        public JavaScriptObject(int value)
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateInt(value);
        }

        public JavaScriptObject(double value)
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateDouble(value);
        }

        public JavaScriptObject(string value)
            : base(typeof (CefV8value), true) {
            var s = new StringUtf16(value);
            NativeHandle = CefV8Capi.CefV8valueCreateString(s.NativeHandle);
            s.Free();
        }

        public JavaScriptObject(DateTime time)
            : base(typeof (CefV8value), true) {
            var t = new Time(time);
            NativeHandle = CefV8Capi.CefV8valueCreateDate(t.NativeHandle);
        }

        public JavaScriptObject(string name, JavaScriptHandler handler)
            : base(typeof (CefV8value), true) {
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
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetValueByindexCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetValueByindex,
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
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetKeysCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetKeys, typeof (GetKeysCallback));
                function(NativeHandle, c.NativeHandle);
                return c;
            }
        }

        public bool IsNull {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsNullCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsNull, typeof (IsNullCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsUndefined {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsUndefinedCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsUndefined,
                                                                     typeof (IsUndefinedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsBoolean {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsBoolCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsBool, typeof (IsBoolCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsString {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsStringCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsString, typeof (IsStringCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsInteger {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsIntCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsInt, typeof (IsIntCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDouble {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsDoubleCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsDouble, typeof (IsDoubleCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsArray {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsArrayCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsArray, typeof (IsArrayCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDate {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsDateCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsDate, typeof (IsDateCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsObject {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsObjectCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsObject, typeof (IsObjectCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsFunction {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsFunctionCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.IsFunction, typeof (IsFunctionCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public int Length {
            get {
                if (!IsArray) {
                    return 0;
                }
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetArrayLengthCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetArrayLength,
                                                                     typeof (GetArrayLengthCallback));
                return function(NativeHandle);
            }
        }

        public JavaScriptObject this[string name] {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetValueBykeyCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetValueBykey,
                                                                     typeof (GetValueBykeyCallback));
                var s = new StringUtf16(name);
                var handle = function(NativeHandle, s.NativeHandle);
                s.Free();
                return FromHandle(handle);
            }
            set {
                var reflection = MarshalFromNative<CefV8value>();
                var action = (SetValueBykeyCallback)
                             Marshal.GetDelegateForFunctionPointer(reflection.SetValueBykey,
                                                                   typeof (SetValueBykeyCallback));
                var s = new StringUtf16(name);
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

        #region IEnumerable<KeyValuePair<string,ScriptableObject>> Members

        public IEnumerator<KeyValuePair<string, JavaScriptObject>> GetEnumerator() {
            return new ScriptableObjectEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        public bool ContainsKey(string key) {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (HasValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.HasValueBykey,
                                                                 typeof (HasValueBykeyCallback));
            var s = new StringUtf16(key);
            var contains = function(NativeHandle, s.NativeHandle);
            s.Free();
            return Convert.ToBoolean(contains);
        }

        public void Add(string key, JavaScriptObject value) {
            var reflection = MarshalFromNative<CefV8value>();
            var action = (SetValueBykeyCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.SetValueBykey,
                                                               typeof (SetValueBykeyCallback));
            var s = new StringUtf16(key);
            action(NativeHandle, s.NativeHandle, value.NativeHandle, CefV8Propertyattribute.V8PropertyAttributeNone);
            s.Free();
        }

        public bool Remove(string key) {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (DeleteValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.DeleteValueBykey,
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
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetStringValueCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.GetStringValue,
                                                                 typeof (GetStringValueCallback));
            var handle = function(NativeHandle);
            return StringUtf16.ReadString(handle);
        }

        public DateTime ToDateTime() {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetDateValueCallback) Marshal.GetDelegateForFunctionPointer(reflection.GetDateValue,
                                                                                        typeof (GetDateValueCallback));
            var handle = function(NativeHandle);
            return Time.FromHandle(handle).ToDateTime();
        }

        public bool ToBoolean() {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetBoolValueCallback) Marshal.GetDelegateForFunctionPointer(reflection.GetBoolValue,
                                                                                        typeof (GetBoolValueCallback));
            var b = function(NativeHandle);
            return Convert.ToBoolean(b);
        }

        public int ToInteger() {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetIntValueCallback) Marshal.GetDelegateForFunctionPointer(reflection.GetIntValue,
                                                                                       typeof (GetIntValueCallback));
            return function(NativeHandle);
        }

        public double ToDouble() {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetDoubleValueCallback)
                           Marshal.GetDelegateForFunctionPointer(reflection.GetDoubleValue,
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

            #region IEnumerator<KeyValuePair<string,ScriptableObject>> Members

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