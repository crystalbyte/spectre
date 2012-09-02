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
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    public abstract class ResourceHandler : RefCountedNativeObject {
        private readonly CanSetCookieCallback _canSetCookieCallback;
        private readonly CanGetCookieCallback _canGetCookieCallback;
        private readonly CancelCallback _cancelCallback;
        private readonly GetResponseHeadersCallback _getResponseHeadersCallback;
        private readonly ProcessRequestCallback _processRequestCallback;
        private readonly ReadResponseCallback _readResponseCallback;

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
                CanGetCookie = Marshal.GetFunctionPointerForDelegate(_canGetCookieCallback),
                CanSetCookie = Marshal.GetFunctionPointerForDelegate(_canSetCookieCallback),
                Cancel = Marshal.GetFunctionPointerForDelegate(_cancelCallback),
                GetResponseHeaders = Marshal.GetFunctionPointerForDelegate(_getResponseHeadersCallback),
                ProcessRequest = Marshal.GetFunctionPointerForDelegate(_processRequestCallback),
                ReadResponse = Marshal.GetFunctionPointerForDelegate(_readResponseCallback)
            });
        }

        private int ReadResponse(IntPtr self, IntPtr dataout, int bytestoread, out int bytesread, IntPtr callback) {
            using (var e = new ResponseDataRequestingEventArgs {
                Controller = AsyncActivityController.FromHandle(callback)
            }) {
                OnResponseDataRequested(e);
                if (e.Controller.IsPaused) {
                    // data retrieval can be resumed, by calling Resume() on the controller
                    bytesread = 0;
                    return 1;
                }

                e.ResponseWriter.Flush();
                e.ResponseWriter.Seek(0, SeekOrigin.Begin);
                using (var reader = new BinaryReader(e.ResponseWriter.BaseStream)) {
                    // TODO: Will break for files larger than 4 GB, split into multiple iterations
                    bytesread = (int) e.ResponseWriter.BaseStream.Length;
                    var bytes = reader.ReadBytes(bytesread);
                    Marshal.Copy(bytes, 0, dataout, bytesread);
                }

                return e.IsCompleted ? 0 : 1;
            }
        }

        private int ProcessRequest(IntPtr self, IntPtr request, IntPtr callback) {
            var e = new ResourceRequestedEventArgs {
                 Controller = AsyncActivityController.FromHandle(callback),
                 Request = Request.FromHandle(request)
            };
            OnResourceRequested(e);
            if (e.IsCanceled) {
                e.Controller.Cancel();
            } else {
                e.Controller.Continue();
            }
            return e.IsCanceled ? 0 : 1;
        }

        public event EventHandler<ResourceRequestedEventArgs> ResourceRequested;

        protected virtual void OnResourceRequested(ResourceRequestedEventArgs e) {
            var handler = ResourceRequested;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void GetResponseHeaders(IntPtr self, IntPtr response, IntPtr responselength, IntPtr redirecturl) {
            

        }

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

        public event EventHandler<ResponseDataRequestingEventArgs> ResponseDataRequested;

        protected virtual void OnResponseDataRequested(ResponseDataRequestingEventArgs e) {
            var handler = ResponseDataRequested;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}