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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class Request : NativeObject {
        public Request()
            : base(typeof (CefRequest)) {
            NativeHandle = CefRequestCapi.CefRequestCreate();
            PostElements = new PostElementCollection();
        }

        private Request(IntPtr handle)
            : base(typeof (CefRequest)) {
            NativeHandle = handle;
        }

        public static Request FromHandle(IntPtr handle) {
            return new Request(handle);
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof (IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public string Method {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (GetMethodCallback) Marshal.GetDelegateForFunctionPointer(r.GetMethod, typeof (GetMethodCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var method = new StringUtf16(value);
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (SetMethodCallback) Marshal.GetDelegateForFunctionPointer(r.SetMethod, typeof (SetMethodCallback));
                action(NativeHandle, method.NativeHandle);
                method.Free();
            }
        }

        public string Url {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function = (GetUrlCallback) Marshal.GetDelegateForFunctionPointer(r.GetUrl, typeof (GetUrlCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var url = new StringUtf16(value);
                var r = MarshalFromNative<CefRequest>();
                var action = (SetUrlCallback) Marshal.GetDelegateForFunctionPointer(r.SetUrl, typeof (SetUrlCallback));
                action(NativeHandle, url.NativeHandle);
            }
        }

        public PostElementCollection PostElements {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (GetPostDataCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetPostData, typeof (GetPostDataCallback));
                var handle = function(NativeHandle);
                return PostElementCollection.FromHandle(handle);
            }
            set {
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (SetPostDataCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetPostData, typeof (SetPostDataCallback));
                action(NativeHandle, value.NativeHandle);
            }
        }

        public IDictionary<string, string> Headers {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public UrlRequestFlags Flags {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (GetFlagsCallback) Marshal.GetDelegateForFunctionPointer(r.GetFlags, typeof (GetFlagsCallback));
                var flags = function(NativeHandle);
                return (UrlRequestFlags) flags;
            }
            set {
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (SetFlagsCallback) Marshal.GetDelegateForFunctionPointer(r.SetFlags, typeof (SetFlagsCallback));
                action(NativeHandle, (int) value);
            }
        }
    }
}