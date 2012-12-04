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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class Response : CefTypeAdapter {
        public Response()
            : base(typeof (CefResponse)) {
            Handle = CefResponseCapi.CefResponseCreate();
        }

        private Response(IntPtr handle)
            : base(typeof (CefResponse)) {
            Handle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function =
                    (CefResponseCapiDelegates.IsReadOnlyCallback6)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                          typeof (CefResponseCapiDelegates.IsReadOnlyCallback6));
                var result = function(Handle);
                return Convert.ToBoolean(result);
            }
        }

        public string MimeType {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (CefResponseCapiDelegates.GetMimeTypeCallback2)
                               Marshal.GetDelegateForFunctionPointer(r.GetMimeType,
                                                                     typeof (
                                                                         CefResponseCapiDelegates.GetMimeTypeCallback2));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var s = new StringUtf16(value);
                var r = MarshalFromNative<CefResponse>();
                var action = (CefResponseCapiDelegates.SetMimeTypeCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetMimeType,
                                                                   typeof (CefResponseCapiDelegates.SetMimeTypeCallback));
                action(Handle, s.Handle);
            }
        }

        public int StatusCode {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (CefResponseCapiDelegates.GetStatusCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetStatus,
                                                                     typeof (CefResponseCapiDelegates.GetStatusCallback));
                return function(Handle);
            }
            set {
                var r = MarshalFromNative<CefResponse>();
                var action = (CefResponseCapiDelegates.SetStatusCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetStatus,
                                                                   typeof (CefResponseCapiDelegates.SetStatusCallback));
                action(Handle, value);
            }
        }

        public string StatusText {
            get {
                var r = MarshalFromNative<CefResponse>();
                var function = (CefResponseCapiDelegates.GetStatusTextCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetStatusText,
                                                                     typeof (
                                                                         CefResponseCapiDelegates.GetStatusTextCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var s = new StringUtf16(value);
                var r = MarshalFromNative<CefResponse>();
                var action = (CefResponseCapiDelegates.SetStatusTextCallback)
                             Marshal.GetDelegateForFunctionPointer(r.SetStatusText,
                                                                   typeof (
                                                                       CefResponseCapiDelegates.SetStatusTextCallback));
                action(Handle, s.Handle);
            }
        }

        internal static Response FromHandle(IntPtr handle) {
            return new Response(handle);
        }
    }
}
