using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public sealed class StreamResourceInfo
    {
        public string ContentType { get; internal set; }
        public Stream Stream { get; internal set; }
    }
}
