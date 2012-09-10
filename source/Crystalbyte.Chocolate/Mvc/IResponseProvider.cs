using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    internal interface IResponseProvider {
        Stream GatherResponse();
    }
}
