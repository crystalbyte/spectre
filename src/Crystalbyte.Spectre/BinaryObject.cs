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
using System.IO;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class BinaryObject : RefCountedCefTypeAdapter {
        public BinaryObject(Stream stream)
            : base(typeof (CefBinaryValue)) {
            if (stream.Length > int.MaxValue) {
                throw new InvalidOperationException("Stream must not exceed size of an 32 bit integer.");
            }
            var length = stream.Length;
            var handle = stream.ToUnmanagedMemory();
            Handle = CefValuesCapi.CefBinaryValueCreate(handle, (int) length);
        }

        public BinaryObject(IntPtr handle)
            : base(typeof (CefBinaryValue)) {
            Handle = handle;
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function =
                    (CefCommandLineCapiDelegates.IsValidCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsValid,
                                                          typeof (CefCommandLineCapiDelegates.IsValidCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsOwned {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (CefValuesCapiDelegates.IsOwnedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsOwned,
                                                                     typeof (CefValuesCapiDelegates.IsOwnedCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public int Size {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function =
                    (CefValuesCapiDelegates.GetSizeCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetSize, typeof (CefValuesCapiDelegates.GetSizeCallback));
                return function(Handle);
            }
        }

        public Stream Data {
            get {
                var s = Size;
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (CefValuesCapiDelegates.GetDataCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetData,
                                                                     typeof (CefValuesCapiDelegates.GetDataCallback));

                var byteSize = Marshal.SizeOf(typeof (byte));
                var handle = Marshal.AllocHGlobal(byteSize*s);
                function(Handle, handle, s, 0);

                var bytes = new byte[s];
                Marshal.Copy(handle, bytes, 0, s);
                Marshal.FreeHGlobal(handle);
                return new MemoryStream(bytes);
            }
        }

        internal static BinaryObject FromHandle(IntPtr handle) {
            return new BinaryObject(handle);
        }

        protected override void DisposeNative() {
            // FIXME: Disposing throws AccessViolationException, object must be disposed internally. 
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            //base.DisposeNative();
        }
    }
}
