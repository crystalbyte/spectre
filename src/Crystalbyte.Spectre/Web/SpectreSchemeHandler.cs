#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class SpectreSchemeHandler : ResourceHandler{
        private readonly IEnumerable<Type> _providerTypes;
        private IDataProvider _provider;

        public SpectreSchemeHandler(IEnumerable<Type> moduleTypes){
            _providerTypes = moduleTypes;
        }

        protected override void OnDataBlockReading(DataBlockReadingEventArgs e){
            Debug.Assert(_provider != null, "_module != null");
            _provider.OnDataBlockReading(e);
        }

        protected override void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e){
            Debug.Assert(_provider != null, "_module != null");
            _provider.OnResponseHeadersReading(e);
        }

        protected override void OnRequestProcessing(RequestProcessingEventArgs e){
            _provider = _providerTypes
                .Select(Activator.CreateInstance)
                .Cast<IDataProvider>()
                .FirstOrDefault(x => x.OnRequestProcessing(e.Request));
            // find a suitable handler to process the request.
            e.IsCanceled = _provider == null;
        }
    }
}