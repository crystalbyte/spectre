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

#endregion

namespace Crystalbyte.Chocolate {
    internal sealed class ObjectCollection : NativeObject {
        public ObjectCollection()
            : base(typeof (CefListValue), true) {
            NativeHandle = CefValuesCapi.CefListValueCreate();
        }

        private ObjectCollection(IntPtr handle)
            : base(typeof (CefListValue), true) {
            NativeHandle = handle;
        }

        public int Count {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (GetSizeCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetSize, typeof (GetSizeCallback));
                return function(NativeHandle);
            }
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (IsReadOnlyCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof (IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool Clear {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (ClearCallback)
                               Marshal.GetDelegateForFunctionPointer(r.Clear, typeof (ClearCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public static ObjectCollection FromHandle(IntPtr handle) {
            return new ObjectCollection(handle);
        }

        public bool SetBinary(int index, BinaryObject bin) {
            var r = MarshalFromNative<CefListValue>();
            var function = (SetBinaryListValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.SetBinary, typeof (SetBinaryListValueCallback));
            var value = function(NativeHandle, index, bin.NativeHandle);
            return Convert.ToBoolean(value);
        }

        public BinaryObject GetBinary(int index) {
            var r = MarshalFromNative<CefListValue>();
            var function = (GetBinaryListValueCallback)
                           Marshal.GetDelegateForFunctionPointer(r.GetBinary, typeof (GetBinaryListValueCallback));
            var handle = function(NativeHandle, index);
            return BinaryObject.FromHandle(handle);
        }


        public void SetSize(int size) {
            var r = MarshalFromNative<CefListValue>();
            var action = (SetSizeCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SetSize, typeof (SetSizeCallback));
            action(NativeHandle, size);
        }

        #region Nested type: GetBinaryListValueCallback

        private delegate IntPtr GetBinaryListValueCallback(IntPtr self, int index);

        #endregion

        #region Nested type: SetBinaryListValueCallback

        private delegate int SetBinaryListValueCallback(IntPtr self, int index, IntPtr value);

        #endregion
    }
}