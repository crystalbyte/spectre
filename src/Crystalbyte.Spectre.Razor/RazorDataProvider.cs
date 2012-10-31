using Crystalbyte.Spectre.Razor.Routing;
using Crystalbyte.Spectre.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public sealed class RazorDataProvider : IDataProvider {
        private Request _request;
        private Response _response;

        public bool OnRequestProcessing(Request request) {
            _request = request;
            return RazorViewRegistrar.CanServe(request.Url);
        }

        public void OnDataBlockReading(DataBlockReadingEventArgs e) {
            throw new NotImplementedException();
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {
            _response = e.Response;

            var controller = RazorViewRegistrar.CreateInstance(_request.Url);
            controller.Initialize(new RequestContext(_request, _response));
            var view = controller.ComposeView();
        }
    }
}
