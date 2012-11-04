using Crystalbyte.Spectre.Web;
using System.IO;
using System.Linq;

namespace Crystalbyte.Spectre.Razor {
    public sealed class RazorDataProvider : IDataProvider, IResponseWriter {
        private Request _request;
        private ActionResult _result;
        private TextWriter _textWriter;

        public bool OnRequestProcessing(Request request) {
            _request = request;
            var name = request.Url.Split('/').Last();
            return ControllerRegistrar.CanServe(name);
        }

        public void OnDataBlockReading(DataBlockReadingEventArgs e) {
            var name = _request.Url.Split('/').Last();
            var context = HostingRuntime.GetContext(name);
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {
            var name = _request.Url.Split('/').Last();
            var controller = ControllerRegistrar.CreateInstance(name);
            controller.Initialize(_request);
            _result = controller.Execute();
            _result.ExecuteResult(new ControllerContext(e, controller, this));
        }
     
        public TextWriter TextWriter {
            get { return _textWriter ?? (_textWriter = new StringWriter()); }
        }
    }
}
