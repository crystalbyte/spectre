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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
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
