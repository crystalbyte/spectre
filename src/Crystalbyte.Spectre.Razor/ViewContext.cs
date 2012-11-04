using System.Collections.Generic;
using Crystalbyte.Spectre.Web;

namespace Crystalbyte.Spectre.Razor {
    public class ViewContext {
        private readonly ControllerContext _context;

        public ViewContext(ControllerContext context) {
            _context = context;
        }

        public ControllerContext ControllerContext {
            get {
                return _context;
            }
        }

        public string ViewName {
            get { 
                return string.Format("{0}View.cshtml", _context
                    .Controller.GetType().Name.Replace("Controller", string.Empty)); 
            }
        }
    }
}