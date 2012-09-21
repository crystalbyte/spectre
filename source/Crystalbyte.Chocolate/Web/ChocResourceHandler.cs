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

namespace Crystalbyte.Chocolate.Web {
    public sealed class ChocResourceHandler : ResourceHandler {
        private Uri _requestUri;
        private IResourceProvider _provider;
        private bool _isInitialized;

        protected override void OnResponseDataReading(ResponseDataReadingEventArgs e) {
            if (!_isInitialized) {
                _provider.Initialize();
                _isInitialized = true;
            }
            e.IsCompleted = _provider.WriteDataBlock(e.ResponseWriter, e.MaxBlockSize);
        }

        protected override void OnResponseHeadersRequested(ResponseHeadersRequestedEventArgs e) {
            var extension = _requestUri.LocalPath.ToFileExtension();
            e.Response.MimeType = MimeMapper.ResolveFromExtension(extension);
            throw new NotImplementedException();

            //if (RouteRegistrar.Current.IsKnownRoute(_requestUri.AbsoluteUri)) {
            //    // View routes do not necessarily correspond to the actual view files.
            //    // We need to manually adjust a views mime type to text/html.
            //    e.Response.MimeType = "text/html";

            //    _provider = new ViewResourceProvider(_requestUri);
            //}
            //else {
            //    _provider = new FileResourceProvider(_requestUri);
            //}

            //try {
            //    var state = _provider.GetResourceState();
            //    switch (state) {
            //        case ResourceState.Valid:
            //            e.Response.StatusCode = 200;
            //            e.Response.StatusText = "OK";
            //            break;
            //        case ResourceState.Missing:
            //            e.Response.StatusCode = 404;
            //            e.Response.StatusText = string.Format("The requested resource has not been found.");
            //            break;
            //        case ResourceState.Locked:
            //            e.Response.StatusCode = 423;
            //            e.Response.StatusText = string.Format("The requested resource is locked and cannot be accessed.");
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }
            //}
            //catch (Exception ex) {
            //    e.Response.StatusCode = 500;
            //    e.Response.StatusText = string.Format("Internal server error. {0}", ex);
            //}
        }

        protected override void OnResourceRequested(ResourceRequestedEventArgs e) {
            _requestUri = new Uri(e.Request.Url);
        }
    }
}