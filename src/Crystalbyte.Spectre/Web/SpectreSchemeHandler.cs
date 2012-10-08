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
using System.Diagnostics;
using System.Linq;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class SpectreSchemeHandler : ResourceHandler {
        private readonly IEnumerable<Type> _providerTypes;
        private IDataProvider _provider;

        public SpectreSchemeHandler(IEnumerable<Type> moduleTypes) {
            _providerTypes = moduleTypes;
        }

        protected override void OnDataBlockReading(DataBlockReadingEventArgs e) {
            Debug.Assert(_provider != null, "_module != null");
            _provider.OnDataBlockReading(e);
        }

        protected override void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {
            Debug.Assert(_provider != null, "_module != null");
            _provider.OnResponseHeadersReading(e);
        }

        protected override void OnRequestProcessing(RequestProcessingEventArgs e) {
            _provider = _providerTypes
                .Select(Activator.CreateInstance)
                .Cast<IDataProvider>()
                .FirstOrDefault(x => x.OnRequestProcessing(e.Request));
            // find a suitable handler to process the request.
            e.IsCanceled = _provider == null;
        }
    }
}