#region Namespace Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    [DebuggerDisplay("Value = {ToString()}")]
    public sealed class ScriptableObject : Adapter, IEnumerable<KeyValuePair<string, ScriptableObject>> {
        private ScriptableObject(IntPtr handle)
            : base(typeof (CefV8value), true) {
            NativeHandle = handle;
        }

        public ScriptableObject()
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateNull();
        }

        public ScriptableObject(bool value)
            : base(typeof (CefV8value), true) {
            var i = Convert.ToInt32(value);
            NativeHandle = CefV8Capi.CefV8valueCreateBool(i);
        }

        public ScriptableObject(int value)
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateInt(value);
        }

        public ScriptableObject(double value)
            : base(typeof (CefV8value), true) {
            NativeHandle = CefV8Capi.CefV8valueCreateDouble(value);
        }

        public ScriptableObject(string value)
            : base(typeof (CefV8value), true) {
            var s = new StringUtf16(value);
            NativeHandle = CefV8Capi.CefV8valueCreateString(s.NativeHandle);
            s.Free();
        }

        public ScriptableObject(DateTime time)
            : base(typeof (CefV8value), true) {
            var t = new Time(time);
            NativeHandle = CefV8Capi.CefV8valueCreateDate(t.NativeHandle);
        }

        public ScriptableObject(string name, ScriptingHandler handler)
            : base(typeof (CefV8value), true) {
            var s = new StringUtf16(name);
            NativeHandle = CefV8Capi.CefV8valueCreateFunction(s.NativeHandle, handler.NativeHandle);
            s.Free();
        }

        public static ScriptableObject Null {
            get {
                var handle = CefV8Capi.CefV8valueCreateNull();
                return new ScriptableObject(handle);
            }
        }

        public static ScriptableObject Undefined {
            get {
                var handle = CefV8Capi.CefV8valueCreateUndefined();
                return new ScriptableObject(handle);
            }
        }

        public ScriptableObject this[int index] {
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

        public ScriptableObject this[string name] {
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

        public IEnumerator<KeyValuePair<string, ScriptableObject>> GetEnumerator() {
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

        public void Add(string key, ScriptableObject value) {
            var reflection = MarshalFromNative<CefV8value>();
            var action = (SetValueBykeyCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.GetValueByindex,
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

        public bool TryGetValue(string key, out ScriptableObject value) {
            if (!ContainsKey(key)) {
                value = null;
                return false;
            }
            value = this[key];
            return true;
        }

        public void Add(KeyValuePair<string, ScriptableObject> item) {
            var reflection = MarshalFromNative<CefV8value>();
            var action = (SetValueBykeyCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.SetValueBykey, typeof (SetValueBykeyCallback));
            var s = new StringUtf16(item.Key);
            action(NativeHandle, s.NativeHandle, item.Value.NativeHandle, CefV8Propertyattribute.V8PropertyAttributeNone);
            s.Free();
        }

        public bool Contains(KeyValuePair<string, ScriptableObject> item) {
            return ContainsKey(item.Key);
        }

        public bool Remove(KeyValuePair<string, ScriptableObject> item) {
            return Remove(item.Key);
        }

        internal static ScriptableObject FromHandle(IntPtr handle) {
            return new ScriptableObject(handle);
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

        public static ScriptableObject CreateArray(int length = 4) {
            var handle = CefV8Capi.CefV8valueCreateArray(length);
            return FromHandle(handle);
        }

        #region Nested type: ScriptableObjectEnumerator

        public sealed class ScriptableObjectEnumerator : IEnumerator<KeyValuePair<string, ScriptableObject>> {
            private readonly int _count;
            private readonly ScriptableObject _so;
            private int _index;

            public ScriptableObjectEnumerator(ScriptableObject so) {
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

            public KeyValuePair<string, ScriptableObject> Current {
                get {
                    var key = _so.Keys[_index];
                    var value = _so[key];
                    return new KeyValuePair<string, ScriptableObject>(key, value);
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