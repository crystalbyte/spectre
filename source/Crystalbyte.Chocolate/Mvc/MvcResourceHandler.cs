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

#endregion

namespace Crystalbyte.Chocolate.Mvc {
    public sealed class MvcResourceHandler : ResourceHandler {
        private Uri _requestRoute;
        private Type _type;
        private bool _isFinished;

        protected override void OnResponseDataReading(ResponseDataReadingEventArgs e) {
            if (_isFinished) {
                e.IsCompleted = true;
                return;
            }

            var controller = (ViewController) Activator.CreateInstance(_type);
            var view = controller.CreateView();
            var markup = view.Compose();

            e.ResponseWriter.Write(markup);
            _isFinished = true;
        }

        protected override void OnResponseHeadersRequested(ResponseHeadersRequestedEventArgs e) {
            e.Response.MimeType = "text/html";

            var success = RouteRegistrar.TryGetController(_requestRoute.AbsolutePath, out _type);
            if (!success) {
                e.Response.StatusCode = 404;
                e.Response.StatusText = string.Format("Document not found @ '{0}'", _requestRoute);
                return;
            }

            e.Response.StatusCode = 200;
            e.Response.StatusText = "OK";
        }

        protected override void OnResourceRequested(ResourceRequestedEventArgs e) {
            _requestRoute = new Uri(e.Request.Url);
        }
    }
}