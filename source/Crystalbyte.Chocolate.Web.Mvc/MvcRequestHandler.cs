using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Web.Mvc
{
    public sealed class MvcRequestHandler : IRequestHandler
    {
        public void OnDataBlockReading(DataBlockReadingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(Request request)
        {
            return false;
        }
    }
}
