#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class ListValue : RefCountedCefTypeAdapter {
        public ListValue()
            : base(typeof (CefListValue)) {
            Handle = CefValuesCapi.CefListValueCreate();
        }

        private ListValue(IntPtr handle)
            : base(typeof (CefListValue)) {
            Handle = handle;
        }

        public int Count {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.GetSizeCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetSize,
                                                                     typeof (CefValuesCapiDelegates.GetSizeCallback));
                return function(Handle);
            }
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.IsReadOnlyCallback7)
                               Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                                     typeof (CefValuesCapiDelegates.IsReadOnlyCallback7));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool Clear {
            get {
                var r = MarshalFromNative<CefListValue>();
                var function = (CefValuesCapiDelegates.ClearCallback2)
                               Marshal.GetDelegateForFunctionPointer(r.Clear,
                                                                     typeof (CefValuesCapiDelegates.ClearCallback2));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public static ListValue FromHandle(IntPtr handle) {
            return new ListValue(handle);
        }

        public bool SetBinary(int index, BinaryObject bin) {
            var r = MarshalFromNative<CefListValue>();
            var function = (CefValuesCapiDelegates.SetBinaryCallback2)
                           Marshal.GetDelegateForFunctionPointer(r.SetBinary,
                                                                 typeof (CefValuesCapiDelegates.SetBinaryCallback2));
            var value = function(Handle, index, bin.Handle);
            return Convert.ToBoolean(value);
        }

        public BinaryObject GetBinary(int index) {
            var r = MarshalFromNative<CefListValue>();
            var function = (CefValuesCapiDelegates.GetBinaryCallback2)
                           Marshal.GetDelegateForFunctionPointer(r.GetBinary,
                                                                 typeof (CefValuesCapiDelegates.GetBinaryCallback2));
            var handle = function(Handle, index);
            return BinaryObject.FromHandle(handle);
        }

        public void SetSize(int size) {
            var r = MarshalFromNative<CefListValue>();
            var action = (CefValuesCapiDelegates.SetSizeCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SetSize,
                                                               typeof (CefValuesCapiDelegates.SetSizeCallback));
            action(Handle, size);
        }
    }
}
