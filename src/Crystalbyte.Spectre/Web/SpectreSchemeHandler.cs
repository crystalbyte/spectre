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
