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

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class ObjectCollection : RefCountedNativeObject {
        public ObjectCollection()
            : base(typeof (CefListValue)) {
            NativeHandle = CefValuesCapi.CefListValueCreate();
        }

        private ObjectCollection(IntPtr handle)
            : base(typeof (CefListValue)) {
            NativeHandle = handle;
        }

        public int Count {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.GetSizeCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetSize,
                                                                     typeof (CefValuesCapiDelegates.GetSizeCallback));
                return function(NativeHandle);
            }
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.IsReadOnlyCallback7)
                               Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                                     typeof (CefValuesCapiDelegates.IsReadOnlyCallback7));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool Clear {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.ClearCallback2)
                               Marshal.GetDelegateForFunctionPointer(r.Clear,
                                                                     typeof (CefValuesCapiDelegates.ClearCallback2));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public static ObjectCollection FromHandle(IntPtr handle) {
            return new ObjectCollection(handle);
        }

        public bool SetBinary(int index, BinaryObject bin) {
            var r = MarshalFromNative<CefListValue>();
            var function = (CefValuesCapiDelegates.SetBinaryCallback2)
                           Marshal.GetDelegateForFunctionPointer(r.SetBinary,
                                                                 typeof (CefValuesCapiDelegates.SetBinaryCallback2));
            var value = function(NativeHandle, index, bin.NativeHandle);
            return Convert.ToBoolean(value);
        }

        public BinaryObject GetBinary(int index) {
            var r = MarshalFromNative<CefListValue>();
            var function = (CefValuesCapiDelegates.GetBinaryCallback2)
                           Marshal.GetDelegateForFunctionPointer(r.GetBinary,
                                                                 typeof (CefValuesCapiDelegates.GetBinaryCallback2));
            var handle = function(NativeHandle, index);
            return BinaryObject.FromHandle(handle);
        }

        public void SetSize(int size) {
            var r = MarshalFromNative<CefListValue>();
            var action = (CefValuesCapiDelegates.SetSizeCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SetSize,
                                                               typeof (CefValuesCapiDelegates.SetSizeCallback));
            action(NativeHandle, size);
        }
    }
}
