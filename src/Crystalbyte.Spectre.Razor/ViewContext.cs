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

        public IList<string> ReferencedAssemblies { get; set; }

        public string ViewName {
            get { 
                return string.Format("Views/{0}View.cshtml", _context
                    .Controller.GetType().Name.Replace("Controller", string.Empty)); 
            }
        }
    }
}