using System.IO;
using Crystalbyte.Spectre.Web;

namespace Crystalbyte.Spectre.Razor {
    public class ControllerContext {
        internal ControllerContext(IResponseContext context, Controller controller, IResponseWriter writer) {
            ResponseWriter = writer;
            ResponseContext = context;
            Controller = controller;
        }

        public IResponseContext ResponseContext { get; private set; }
        public Controller Controller { get; private set; }
        public IResponseWriter ResponseWriter { get; set; }
    }
}