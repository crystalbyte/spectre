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
using System.IO;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class BinaryObject : RefCountedNativeObject {
        public BinaryObject(Stream stream)
            : base(typeof (CefBinaryValue)) {
            var length = (int) stream.Length;
            var handle = stream.ToUnmanagedMemory();
            NativeHandle = CefValuesCapi.CefBinaryValueCreate(handle, length);
        }

        public BinaryObject(IntPtr handle)
            : base(typeof (CefBinaryValue)) {
            NativeHandle = handle;
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function =
                    (IsValidCallback) Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof (IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsOwned {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (IsOwnedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsOwned, typeof (IsOwnedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public int Size {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function =
                    (GetSizeCallback) Marshal.GetDelegateForFunctionPointer(r.GetSize, typeof (GetSizeCallback));
                return function(NativeHandle);
            }
        }

        public Stream Data {
            get {
                var s = Size;
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (GetDataCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetData, typeof (GetDataCallback));

                var byteSize = Marshal.SizeOf(typeof (byte));
                var handle = Marshal.AllocHGlobal(byteSize*s);
                function(NativeHandle, handle, s, 0);

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
            // Check if problem still occurs.
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            base.DisposeNative();
        }
    }
}
