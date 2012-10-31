using Crystalbyte.Spectre.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor.Routing {
    public sealed class RequestContext {
        private readonly Response _response;
        private readonly Request _request;

        public RequestContext(Request request, Response response) {
            _response = response;
            _request = request;
        }

        public Request Request { get { return _request; } }
        public Response Response { get { return _response; } }
    }
}
