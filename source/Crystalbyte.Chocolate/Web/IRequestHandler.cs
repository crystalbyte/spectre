using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Web
{
    public interface IRequestHandler {
        void OnDataBlockReading(DataBlockReadingEventArgs e);
        void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e);
        bool CanHandle(Request request);

    }
}
