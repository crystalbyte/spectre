#region Namespace Directives

using System;
using System.IO;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class BinaryObject : Adapter {
        public BinaryObject(Stream stream)
            : base(typeof (CefBinaryValue), true) {
            var length = (int) stream.Length;
            var handle = stream.ToUnmanagedMemory();
            NativeHandle = CefValuesCapi.CefBinaryValueCreate(handle, length);
        }

        public BinaryObject(IntPtr handle)
            : base(typeof (CefBinaryValue), true) {
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
                var handle = Marshal.AllocHGlobal(byteSize * s);
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
            // TODO: check thread for progress 
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            return;
            //base.DisposeNative();
        }
    }
}