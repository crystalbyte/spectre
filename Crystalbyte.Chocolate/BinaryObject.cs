using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate
{
    public sealed class BinaryObject : Adapter {
        public BinaryObject(Stream stream) 
            : base(typeof(CefBinaryValue), true) {
            var length = (int) stream.Length;
            var handle = stream.ToUnmanagedMemory();
            NativeHandle = CefValuesCapi.CefBinaryValueCreate(handle, length);
        }

        public BinaryObject(IntPtr handle)
            : base(typeof(CefBinaryValue), true) {
            NativeHandle = handle;
        }

        internal static BinaryObject FromHandle(IntPtr handle) {
            return new BinaryObject(handle);
        }

        public bool IsValid {
            get { 
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (IsValidCallback)Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof(IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public int Size {
            get {
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (GetSizeCallback)Marshal.GetDelegateForFunctionPointer(r.GetSize, typeof(GetSizeCallback));
                return function(NativeHandle);
            }
        }

        public Stream Data {
            get {
                var s = Size;
                var r = MarshalFromNative<CefBinaryValue>();
                var function = (GetDataCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetData, typeof(GetDataCallback));

                var byteSize = Marshal.SizeOf(typeof(byte));
                var handle = Marshal.AllocHGlobal(byteSize * s);
                function(NativeHandle, handle, s, 0);

                var bytes = new byte[s];
                Marshal.Copy(handle, bytes, 0, s);
                Marshal.FreeHGlobal(handle);
                return new MemoryStream(bytes);
            }
        }
    }
}
