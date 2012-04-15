using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate
{
    public sealed class IpcMessage : Adapter
    {
        public IpcMessage(string name) 
            : base(typeof(CefProcessMessage), true) {
            var s = new StringUtf16(name);
            NativeHandle = CefProcessMessageCapi.CefProcessMessageCreate(s.NativeHandle);
            s.Free();
        }

        private IpcMessage(IntPtr handle)
            : base(typeof(CefProcessMessage)) {
            NativeHandle = handle;
        }

        public static IpcMessage FromHandle(IntPtr handle) {
            return new IpcMessage(handle);
        }

        public bool IsReadOnly {
            get { 
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof(IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }
        protected override void DisposeNative() {
            // TODO: check thread for progress 
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            return;
            //base.DisposeNative();
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (IsValidCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof(IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public string Name {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (GetNameCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetName, typeof(GetNameCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        private ObjectCollection Arguments {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function =
                    (GetArgumentListCallback)Marshal.GetDelegateForFunctionPointer(r.GetArgumentList, typeof(GetArgumentListCallback));
                var handle = function(NativeHandle);
                return ObjectCollection.FromHandle(handle);
            }
        }

        public Stream Payload {
            get { 
                var bin = Arguments.GetBinary(0);
                return bin.Data;
            }
            set {
                var a = Arguments;
                if (a.Count < 1) {
                    a.SetSize(1);
                }
                Arguments.SetBinary(0, new BinaryObject(value));
            }
        }
    }
}
