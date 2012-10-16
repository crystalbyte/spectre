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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
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

        public static Request FromHandle(IntPtr handle) {
            return new Request(handle);
        }
    }
}
