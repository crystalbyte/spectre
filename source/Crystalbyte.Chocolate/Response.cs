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
    public sealed class Response : NativeObject {
        public Response()
            : base(typeof (CefResponse)) {
            NativeHandle = CefResponseCapi.CefResponseCreate();
        }

        private Response(IntPtr handle)
            : base(typeof (CefResponse)) {
            NativeHandle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function =
                    (IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof (IsReadOnlyCallback));
                var result = function(NativeHandle);
                return Convert.ToBoolean(result);
            }
        }

        public string MimeType {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (GetMimeTypeCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMimeType, typeof (GetMimeTypeCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var s = new StringUtf16(value);
                var r = MarshalFromNative<CefResponse>();
                var action = (SetMimeTypeCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetMimeType, typeof (SetMimeTypeCallback));
                action(NativeHandle, s.NativeHandle);
            }
        }

        public int StatusCode {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (GetStatusCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetStatus, typeof (GetStatusCallback));
                return function(NativeHandle);
            }
            set {
                var r = MarshalFromNative<CefResponse>();
                var action = (SetStatusCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetStatus, typeof (SetStatusCallback));
                action(NativeHandle, value);
            }
        }

        public string StatusText {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (GetStatusTextCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetStatusText, typeof (GetStatusTextCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var s = new StringUtf16(value);
                var r = MarshalFromNative<CefResponse>();
                var action = (SetStatusTextCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetStatusText, typeof (SetStatusTextCallback));
                action(NativeHandle, s.NativeHandle);
            }
        }

        internal static Response FromHandle(IntPtr handle) {
            return new Response(handle);
        }
    }
}