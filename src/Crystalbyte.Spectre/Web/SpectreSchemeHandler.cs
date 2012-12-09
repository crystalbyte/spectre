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
