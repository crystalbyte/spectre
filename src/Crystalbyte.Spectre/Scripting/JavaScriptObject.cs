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
    public sealed class JavaScriptObject : RefCountedCefTypeAdapter, IEnumerable<KeyValuePair<string, JavaScriptObject>>,
                                           IFunction {
        private JavaScriptObject(IntPtr handle)
            : base(typeof (CefV8value)) {
            Handle = handle;
        }

        protected override void DisposeNative() {
            base.DisposeNative();
        }

        public JavaScriptObject(bool value)
            : base(typeof (CefV8value)) {
            var i = Convert.ToInt32(value);
            Handle = CefV8Capi.CefV8valueCreateBool(i);
        }

        public JavaScriptObject(int value)
            : base(typeof (CefV8value)) {
            Handle = CefV8Capi.CefV8valueCreateInt(value);
        }

        public JavaScriptObject(double value)
            : base(typeof (CefV8value)) {
            Handle = CefV8Capi.CefV8valueCreateDouble(value);
        }

        public JavaScriptObject(string value)
            : base(typeof (CefV8value)) {
            var s = new StringUtf16(value);
            Handle = CefV8Capi.CefV8valueCreateString(s.Handle);
            s.Free();
        }

        public JavaScriptObject(DateTime time)
            : base(typeof (CefV8value)) {
            var t = new Time(time);
            Handle = CefV8Capi.CefV8valueCreateDate(t.Handle);
        }

        public JavaScriptObject(string name, JavaScriptHandler handler)
            : base(typeof (CefV8value)) {
            var s = new StringUtf16(name);
            Handle = CefV8Capi.CefV8valueCreateFunction(s.Handle, handler.Handle);
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
                var function = (CefV8CapiDelegates.GetValueByindexCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetValueByindex,
                                                                     typeof (CefV8CapiDelegates.GetValueByindexCallback));
                var handle = function(Handle, index);
                return FromHandle(handle);
            }
        }

        public IValueCollection<string> Keys {
            get {
                if (!IsArray) {
                    return null;
                }
                var c = new StringUtf16List();
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.GetKeysCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetKeys,
                                                                     typeof (CefV8CapiDelegates.GetKeysCallback));
                function(Handle, c.Handle);
                return c;
            }
        }

        public bool IsNull {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsNullCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsNull,
                                                                     typeof (CefV8CapiDelegates.IsNullCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsUndefined {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsUndefinedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsUndefined,
                                                                     typeof (CefV8CapiDelegates.IsUndefinedCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsBoolean {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsBoolCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsBool,
                                                                     typeof (CefV8CapiDelegates.IsBoolCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsString {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsStringCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsString,
                                                                     typeof (CefV8CapiDelegates.IsStringCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsInteger {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsIntCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsInt, typeof (CefV8CapiDelegates.IsIntCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDouble {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsDoubleCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsDouble,
                                                                     typeof (CefV8CapiDelegates.IsDoubleCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsArray {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsArrayCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsArray,
                                                                     typeof (CefV8CapiDelegates.IsArrayCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsDate {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsDateCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsDate,
                                                                     typeof (CefV8CapiDelegates.IsDateCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsObject {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsObjectCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsObject,
                                                                     typeof (CefV8CapiDelegates.IsObjectCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsFunction {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.IsFunctionCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsFunction,
                                                                     typeof (CefV8CapiDelegates.IsFunctionCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public int Length {
            get {
                if (!IsArray) {
                    return 0;
                }
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.GetArrayLengthCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetArrayLength,
                                                                     typeof (CefV8CapiDelegates.GetArrayLengthCallback));
                return function(Handle);
            }
        }

        public JavaScriptObject this[string name] {
            get {
                var r = MarshalFromNative<CefV8value>();
                var function = (CefV8CapiDelegates.GetValueBykeyCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetValueBykey,
                                                                     typeof (CefV8CapiDelegates.GetValueBykeyCallback));
                var s = new StringUtf16(name);
                var handle = function(Handle, s.Handle);
                s.Free();
                return FromHandle(handle);
            }
            set {
                var r = MarshalFromNative<CefV8value>();
                var action = (CefV8CapiDelegates.SetValueBykeyCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetValueBykey,
                                                                   typeof (CefV8CapiDelegates.SetValueBykeyCallback));
                var s = new StringUtf16(name);

                Reference.Increment(value);
                action(Handle, s.Handle, value.Handle, CefV8Propertyattribute.V8PropertyAttributeNone);
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
                var function = (CefV8CapiDelegates.GetFunctionNameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFunctionName,
                                                                     typeof (CefV8CapiDelegates.GetFunctionNameCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

//        JavaScriptHandler IFunction.Handler {
//            get {
//                var r = MarshalFromNative<CefV8value>();
//                var function = (CefV8CapiDelegates.GetFunctionHandlerCallback)
//                               Marshal.GetDelegateForFunctionPointer(r.GetFunctionHandler,
//                                                                     typeof (
//                                                                         CefV8CapiDelegates.GetFunctionHandlerCallback));
//                var handle = function(Handle);
//                return JavaScriptHandler.FromHandle(handle);
//            }
//        }

        JavaScriptObject IFunction.Execute(params JavaScriptObject[] arguments) {
            return (this as IFunction).ExecuteWithTarget(Null, arguments);
        }

        JavaScriptObject IFunction.ExecuteWithTarget(JavaScriptObject target, params JavaScriptObject[] arguments) {
            if (target == null)
                throw new ArgumentNullException("target");

            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.ExecuteFunctionCallback)
                           Marshal.GetDelegateForFunctionPointer(r.ExecuteFunction,
                                                                 typeof (CefV8CapiDelegates.ExecuteFunctionCallback));
            var handle = arguments.ToUnmanagedArray();

            Reference.Increment(target.Handle);
            arguments.ForEach(Reference.Increment);

            var resultHandle = function(Handle, target.Handle, arguments.Length, handle);

            var result = FromHandle(resultHandle);
            return result;
        }

        JavaScriptObject IFunction.ExecuteWithContextAndTarget(ScriptingContext context, JavaScriptObject target,
                                                               params JavaScriptObject[] arguments) {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.IsDisposed) {
                return null;
            }

            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.ExecuteFunctionWithContextCallback)
                           Marshal.GetDelegateForFunctionPointer(r.ExecuteFunctionWithContext,
                                                                 typeof (
                                                                     CefV8CapiDelegates.
                                                                     ExecuteFunctionWithContextCallback));
            var handle = arguments.ToUnmanagedArray();

            Reference.Increment(context.Handle);
            Reference.Increment(target.Handle);
            arguments.ForEach(Reference.Increment);

            var resultHandle = function(Handle, context.Handle, target.Handle, arguments.Length,
                                        handle);

            var result = FromHandle(resultHandle);
            return result;
        }

        #endregion

        public bool ContainsKey(string key) {
            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.HasValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(r.HasValueBykey,
                                                                 typeof (CefV8CapiDelegates.HasValueBykeyCallback));
            var s = new StringUtf16(key);
            var contains = function(Handle, s.Handle);
            s.Free();
            return Convert.ToBoolean(contains);
        }

        public void Add(string key, JavaScriptObject value) {
            var r = MarshalFromNative<CefV8value>();
            var action = (CefV8CapiDelegates.SetValueBykeyCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SetValueBykey,
                                                               typeof (CefV8CapiDelegates.SetValueBykeyCallback));
            var s = new StringUtf16(key);
            Reference.Increment(value);
            action(Handle, s.Handle, value.Handle, CefV8Propertyattribute.V8PropertyAttributeNone);
            s.Free();
        }

        public bool Remove(string key) {
            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.DeleteValueBykeyCallback)
                           Marshal.GetDelegateForFunctionPointer(r.DeleteValueBykey,
                                                                 typeof (CefV8CapiDelegates.DeleteValueBykeyCallback));
            var s = new StringUtf16(key);
            var success = function(Handle, s.Handle);
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
            var function = (CefV8CapiDelegates.GetStringValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.GetStringValue,
                                                                 typeof (CefV8CapiDelegates.GetStringValueCallback));
            var handle = function(Handle);
            return StringUtf16.ReadString(handle);
        }

        public DateTime ToDateTime() {
            var r = MarshalFromNative<CefV8value>();
            var function =
                (CefV8CapiDelegates.GetDateValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetDateValue,
                                                                                                typeof (
                                                                                                    CefV8CapiDelegates.
                                                                                                    GetDateValueCallback
                                                                                                    ));
            var handle = function(Handle);
            return Time.FromHandle(handle).ToDateTime();
        }

        public bool ToBoolean() {
            var r = MarshalFromNative<CefV8value>();
            var function =
                (CefV8CapiDelegates.GetBoolValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetBoolValue,
                                                                                                typeof (
                                                                                                    CefV8CapiDelegates.
                                                                                                    GetBoolValueCallback
                                                                                                    ));
            var b = function(Handle);
            return Convert.ToBoolean(b);
        }

        public int ToInteger() {
            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.GetIntValueCallback) Marshal.GetDelegateForFunctionPointer(r.GetIntValue,
                                                                                                          typeof (
                                                                                                              CefV8CapiDelegates
                                                                                                              .
                                                                                                              GetIntValueCallback
                                                                                                              ));
            return function(Handle);
        }

        public IFunction ToFunction() {
            return this;
        }

        public double ToDouble() {
            var r = MarshalFromNative<CefV8value>();
            var function = (CefV8CapiDelegates.GetDoubleValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.GetDoubleValue,
                                                                 typeof (CefV8CapiDelegates.GetDoubleValueCallback));
            return function(Handle);
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
