using System;
using Crystalbyte.Spectre.Web;
using System.IO;
using System.Linq;

namespace Crystalbyte.Spectre.Razor {
    public sealed class RazorDataProvider : IDataProvider, IResponseWriter {
        private Request _request;
        private ActionResult _result;
        private TextWriter _writer;
        private StringReader _reader;

        public bool OnRequestProcessing(Request request) {
            _request = request;
            var name = request.Url.Split('/').Last();
            return ControllerRegistrar.CanServe(name);
        }

        public void OnDataBlockReading(DataBlockReadingEventArgs e) {
            var buffer = new char[e.MaxBlockSize / 2];
            var length = _reader.Read(buffer, 0, e.MaxBlockSize / 2);
            if (length == 0) {
                e.IsCompleted = true;
                CleanUp();
            } else {
                e.ResponseWriter.Write(buffer, 0, length);    
            }
        }

        private void CleanUp() {
            if (_reader != null) {
                _reader.Dispose();
            }
            if (_writer != null) {
                _writer.Dispose();
            }
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {
            
            try {
                var name = _request.Url.Split('/').Last();
                var controller = ControllerRegistrar.CreateInstance(name);
                controller.Initialize(_request);
                _result = controller.Execute();
                _result.ExecuteResult(new ControllerContext(e, controller, this));

                e.Response.MimeType = "text/html";
                e.Response.StatusCode = 200;
                e.Response.StatusText = "OK";
            }
            catch (Exception ex) {
                e.Response.MimeType = "text/html";
                e.Response.StatusCode = 500;
                e.Response.StatusText = "Internal Error";

                _reader = new StringReader(string.Format("<body style=\"background-color: #252525; color: whitesmoke;\"><pre>{0}</pre></body>", ex));
            }
        }
     
        public TextWriter TextWriter {
            get { return _writer ?? (_writer = new StringWriter()); }
        }

        public void Finish() {
            _reader = new StringReader(_writer.ToString());
        }
    }
}
