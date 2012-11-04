using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public interface IResponseWriter {
        TextWriter TextWriter { get; }
        void Finish();
    }
}
