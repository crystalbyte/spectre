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
    public sealed class Request : CefTypeAdapter {
        public Request()
            : base(typeof (CefRequest)) {
            Handle = CefRequestCapi.CefRequestCreate();
            PostElements = new PostElementCollection();
        }

        private Request(IntPtr handle)
            : base(typeof (CefRequest)) {
            Handle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (CefCommandLineCapiDelegates.IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                          typeof (CefCommandLineCapiDelegates.IsReadOnlyCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public string Method {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (CefRequestCapiDelegates.GetMethodCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetMethod,
                                                          typeof (CefRequestCapiDelegates.GetMethodCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var method = new StringUtf16(value);
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (CefRequestCapiDelegates.SetMethodCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetMethod,
                                                          typeof (CefRequestCapiDelegates.SetMethodCallback));
                action(Handle, method.Handle);
                method.Free();
            }
        }

        public string Url {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (CefDownloadItemCapiDelegates.GetUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetUrl, typeof (CefDownloadItemCapiDelegates.GetUrlCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var url = new StringUtf16(value);
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (CefRequestCapiDelegates.SetUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetUrl, typeof (CefRequestCapiDelegates.SetUrlCallback));
                action(Handle, url.Handle);
            }
        }

        public PostElementCollection PostElements {
            get {
                var r = MarshalFromNative<CefRequest>();
                var function =
                    (CefRequestCapiDelegates.GetPostDataCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetPostData,
                                                          typeof (CefRequestCapiDelegates.GetPostDataCallback));
                var handle = function(Handle);
                return PostElementCollection.FromHandle(handle);
            }
            set {
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (CefRequestCapiDelegates.SetPostDataCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetPostData,
                                                          typeof (CefRequestCapiDelegates.SetPostDataCallback));
                action(Handle, value.Handle);
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
                    (CefRequestCapiDelegates.GetFlagsCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetFlags, typeof (CefRequestCapiDelegates.GetFlagsCallback));
                var flags = function(Handle);
                return (UrlRequestFlags) flags;
            }
            set {
                var r = MarshalFromNative<CefRequest>();
                var action =
                    (CefRequestCapiDelegates.SetFlagsCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetFlags, typeof (CefRequestCapiDelegates.SetFlagsCallback));
                action(Handle, (int) value);
            }
        }

        public static Request FromHandle(IntPtr handle) {
            return new Request(handle);
        }
    }
}
