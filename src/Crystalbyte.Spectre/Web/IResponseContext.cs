using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Web {
    public interface IResponseContext {
        Response Response { get; }
        Uri RedirectUri { get; set; }
    }
}
