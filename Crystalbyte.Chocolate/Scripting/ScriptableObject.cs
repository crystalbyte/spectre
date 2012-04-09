#region Namespace Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.Scripting {
    [DebuggerDisplay("Value = {ToString()}")]
    public sealed class ScriptableObject : Adapter, IDictionary<string, ScriptableObject> {
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
            var str = new StringUtf16(value);
            NativeHandle = CefV8Capi.CefV8valueCreateString(str.NativeHandle);
        }

        public ScriptableObject(DateTime time)
            : base(typeof (CefV8value), true) {
            var t = new Time(time);
            NativeHandle = CefV8Capi.CefV8valueCreateDate(t.NativeHandle);
        }

        public ScriptableObject(string name, ScriptingHandler handler)
            : base(typeof (CefV8value), true) {
            var n = new StringUtf16(name);
            NativeHandle = CefV8Capi.CefV8valueCreateFunction(n.NativeHandle, handler.NativeHandle);
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
                    Marshal.GetDelegateForFunctionPointer(reflection.GetValueByindex, typeof (GetValueByindexCallback));
                var handle = function(NativeHandle, index);
                return FromHandle(handle);
            }
        }

        public bool ContainsKey(string key) {
            throw new NotImplementedException();
        }

        public void Add(string key, ScriptableObject value) {
            throw new NotImplementedException();
        }

        public bool Remove(string key) {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out ScriptableObject value) {
            throw new NotImplementedException();
        }

        ScriptableObject IDictionary<string, ScriptableObject>.this[string key] {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ICollection<string> Keys {
            get { throw new NotImplementedException(); }
        }

        public ICollection<ScriptableObject> Values {
            get { throw new NotImplementedException(); }
        }

        public ScriptableObject this[string name] {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetValueBykeyCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetValueBykey, typeof(GetValueBykeyCallback));
                var str = new StringUtf16(name);
                var handle = function(NativeHandle, str.NativeHandle);
                return FromHandle(handle);
            }
        }

        public bool IsNull {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsNullCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsNull, typeof(IsNullCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsUndefined {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsUndefinedCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsUndefined, typeof(IsUndefinedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsBoolean {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsBoolCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsBool, typeof(IsBoolCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsString {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsStringCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsString, typeof(IsStringCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsInteger {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsIntCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsInt, typeof(IsIntCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDouble {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsDoubleCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsDouble, typeof(IsDoubleCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsArray {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsArrayCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsArray, typeof(IsArrayCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDate {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsDateCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsDate, typeof(IsDateCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsObject {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsObjectCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsObject, typeof(IsObjectCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsFunction {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (IsFunctionCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsFunction, typeof(IsFunctionCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public int Length {
            get {
                var reflection = MarshalFromNative<CefV8value>();
                var function = (GetArrayLengthCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetArrayLength,
                                                                     typeof (GetArrayLengthCallback));
                return function(NativeHandle);
            }
        }

        internal static ScriptableObject FromHandle(IntPtr handle) {
            return new ScriptableObject(handle);
        }

        public sealed class ScriptableObjectEnumerator : IEnumerator<ScriptableObject> {
            private readonly IntPtr _handle;
            private readonly GetValueByindexCallback _function;
            private readonly int _count;
            private int _index;

            public ScriptableObjectEnumerator(ScriptableObject so) {
                var reflection = so.MarshalFromNative<CefV8value>();
                _function = (GetValueByindexCallback) 
                    Marshal.GetDelegateForFunctionPointer(reflection.GetValueByindex, typeof (GetValueByindexCallback));
                _handle = so.NativeHandle;
                _count = so.Length;
                _index = 0;
            }

            public void Dispose() {
                // nada
            }

            public bool MoveNext() {
                return _index++ < _count;
            }

            public void Reset() {
                _index = 0;
            }

            public ScriptableObject Current {
                get {
                    var handle = _function(_handle, _index);
                    return ScriptableObject.FromHandle(handle);
                }
            }

            object IEnumerator.Current {
                get { return Current; }
            }
        }

        public IEnumerator<KeyValuePair<string, ScriptableObject>> GetEnumerator() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            var reflection = MarshalFromNative<CefV8value>();
            var function = (GetStringValueCallback) 
                Marshal.GetDelegateForFunctionPointer(reflection.GetStringValue, typeof (GetStringValueCallback));
            var handle = function(NativeHandle);
            return StringUtf16.ReadString(handle);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
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
                Marshal.GetDelegateForFunctionPointer(reflection.GetDoubleValue, typeof (GetDoubleValueCallback));
            return function(NativeHandle);
        }

        public static ScriptableObject CreateArray(int length = 4) {
            var handle = CefV8Capi.CefV8valueCreateArray(length);
            return FromHandle(handle);
        }

        public void Add(KeyValuePair<string, ScriptableObject> item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, ScriptableObject> item) {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, ScriptableObject>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, ScriptableObject> item) {
            throw new NotImplementedException();
        }

        public int Count {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly {
            get { throw new NotImplementedException(); }
        }
    }
}