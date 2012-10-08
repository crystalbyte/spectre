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
using System.IO;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class IpcMessage : NativeObject {
        public IpcMessage(string name)
            : base(typeof (CefProcessMessage), true) {
            var s = new StringUtf16(name);
            NativeHandle = CefProcessMessageCapi.CefProcessMessageCreate(s.NativeHandle);
            s.Free();
        }

        private IpcMessage(IntPtr handle)
            : base(typeof (CefProcessMessage)) {
            NativeHandle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (IsReadOnlyCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof (IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (IsValidCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof (IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public string Name {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (GetNameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetName, typeof (GetNameCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        private ObjectCollection Arguments {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function =
                    (GetArgumentListCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetArgumentList, typeof (GetArgumentListCallback));
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

        public static IpcMessage FromHandle(IntPtr handle) {
            return new IpcMessage(handle);
        }

        protected override void DisposeNative() {
            // TODO: Check thread for progress.
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            base.DisposeNative();
        }
    }
}