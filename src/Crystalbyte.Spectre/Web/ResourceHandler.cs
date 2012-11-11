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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public abstract class ResourceHandler : OwnedRefCountedNativeObject {
        private readonly CefResourceHandlerCapiDelegates.CanGetCookieCallback _canGetCookieCallback;
        private readonly CefResourceHandlerCapiDelegates.CanSetCookieCallback _canSetCookieCallback;
        private readonly CefResourceHandlerCapiDelegates.CancelCallback6 _cancelCallback;
        private readonly CefResourceHandlerCapiDelegates.GetResponseHeadersCallback _getResponseHeadersCallback;
        private readonly CefResourceHandlerCapiDelegates.ProcessRequestCallback _processRequestCallback;
        private readonly CefResourceHandlerCapiDelegates.ReadResponseCallback _readResponseCallback;

        protected ResourceHandler()
            : base(typeof (CefResourceHandler)) {
            _canSetCookieCallback = CanSetCookie;
            _canGetCookieCallback = CanGetCookie;
            _cancelCallback = Cancel;
            _getResponseHeadersCallback = GetResponseHeaders;
            _processRequestCallback = ProcessRequest;
            _readResponseCallback = ReadResponse;

            MarshalToNative(new CefResourceHandler {
                Base = DedicatedBase,
                CanGetCookie =
                    Marshal.GetFunctionPointerForDelegate(_canGetCookieCallback),
                CanSetCookie =
                    Marshal.GetFunctionPointerForDelegate(_canSetCookieCallback),
                Cancel = Marshal.GetFunctionPointerForDelegate(_cancelCallback),
                GetResponseHeaders =
                    Marshal.GetFunctionPointerForDelegate(
                        _getResponseHeadersCallback),
                ProcessRequest =
                    Marshal.GetFunctionPointerForDelegate(_processRequestCallback),
                ReadResponse =
                    Marshal.GetFunctionPointerForDelegate(_readResponseCallback)
            });
        }

        private int ReadResponse(IntPtr self, IntPtr dataout, int bytestoread, ref int bytesread, IntPtr callback) {
            using (var writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8)) {
                var e = new DataBlockReadingEventArgs(writer) {
                    MaxBlockSize = bytestoread,
                    DelayController =
                        ResponseDelayController.FromHandle(callback)
                };

                OnDataBlockReading(e);

                if (e.DelayController.IsPaused) {
                    // Data retrieval can be resumed by calling Resume() on the controller.
                    bytesread = 0;
                    return 1;
                }

                e.ResponseWriter.Flush();
                e.ResponseWriter.Seek(0, SeekOrigin.Begin);

                if (e.ResponseWriter.BaseStream.Length == 0) {
                    bytesread = 0;
                    return 0;
                }

                using (var reader = new BinaryReader(e.ResponseWriter.BaseStream)) {
                    var bytes = reader.ReadBytes(e.MaxBlockSize);
                    bytesread = bytes.Length;
                    Marshal.Copy(bytes, 0, dataout, bytesread);
                }

                return e.IsCompleted ? 0 : 1;
            }
        }

        private int ProcessRequest(IntPtr self, IntPtr request, IntPtr callback) {
            var e = new RequestProcessingEventArgs {
                Controller = AsyncActivityController.FromHandle(callback),
                Request = Request.FromHandle(request)
            };
            OnRequestProcessing(e);
            if (e.IsCanceled) {
                e.Controller.Cancel();
            }
            else {
                e.Controller.Continue();
            }

            e.Controller.Dispose();
            return e.IsCanceled ? 0 : 1;
        }

        public event EventHandler<RequestProcessingEventArgs> ResourceRequested;

        protected virtual void OnRequestProcessing(RequestProcessingEventArgs e) {
            var handler = ResourceRequested;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void GetResponseHeaders(IntPtr self, IntPtr response, ref long responselength, IntPtr redirecturl) {
            var e = new ResponseHeadersReadingEventArgs {
                Response = Response.FromHandle(response)
            };

            OnResponseHeadersReading(e);

            if (e.RedirectUri != null) {
                StringUtf16.WriteString(e.RedirectUri.AbsoluteUri, redirecturl);
            }

            // We will pass the data as a stream, its length will not be determined at this point.
            responselength = -1;
        }

        protected virtual void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {}

        private void Cancel(IntPtr self) {}

        private int CanGetCookie(IntPtr self, IntPtr cookie) {
            return 0;
        }

        private int CanSetCookie(IntPtr self, IntPtr cookie) {
            return 0;
        }

        public event EventHandler Canceled;

        protected virtual void OnCanceled(EventArgs e) {
            var handler = Canceled;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler<DataBlockReadingEventArgs> ResponseDataReading;

        protected virtual void OnDataBlockReading(DataBlockReadingEventArgs e) {
            var handler = ResponseDataReading;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}
