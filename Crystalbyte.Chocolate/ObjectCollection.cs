using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate
{
    internal sealed class ObjectCollection : Adapter
    {
        public ObjectCollection() 
            : base(typeof(CefListValue), true) {
            NativeHandle = CefValuesCapi.CefListValueCreate();
        }

        private ObjectCollection(IntPtr handle) 
            : base(typeof(CefListValue), true) {
            NativeHandle = handle;
        }

        public static ObjectCollection FromHandle(IntPtr handle) {
            return new ObjectCollection(handle);
        }

        public int Count {
            get { 
                var r = MarshalFromNative<CefListValue>();
                var function = (GetSizeCallback) 
                    Marshal.GetDelegateForFunctionPointer(r.GetSize, typeof(GetSizeCallback));
                return function(NativeHandle);
            }
        }

        private delegate int SetBinaryListValueCallback(IntPtr self, int index, IntPtr value);
        public bool SetBinary(int index, BinaryObject bin) {
            var r = MarshalFromNative<CefListValue>();
            var function = (SetBinaryListValueCallback) 
                Marshal.GetDelegateForFunctionPointer(r.SetBinary, typeof(SetBinaryListValueCallback));
            var value = function(NativeHandle, index, bin.NativeHandle);
            return Convert.ToBoolean(value);
        }

        private delegate IntPtr GetBinaryListValueCallback(IntPtr self, int index);
        public BinaryObject GetBinary(int index)
        {
            var r = MarshalFromNative<CefListValue>();
            var function = (GetBinaryListValueCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetBinary, typeof(GetBinaryListValueCallback));
            var handle = function(NativeHandle, index);
            return BinaryObject.FromHandle(handle);
        }

        public bool IsReadOnly {
            get { 
                var r = MarshalFromNative<CefListValue>();
                var function = (IsReadOnlyCallback) 
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof(IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool Clear {
            get { 
                var r = MarshalFromNative<CefListValue>();
                var function = (ClearCallback)
                    Marshal.GetDelegateForFunctionPointer(r.Clear, typeof(ClearCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }


        public void SetSize(int size) {
            var r = MarshalFromNative<CefListValue>();
            var action = (SetSizeCallback)
                Marshal.GetDelegateForFunctionPointer(r.SetSize, typeof(SetSizeCallback));
            action(NativeHandle, size);
        }
    }
}
