using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Crystalbyte.Chocolate.Web.Mvc
{
    public sealed class AspRequestProcessor : MarshalByRefObject
    {
        public string ProcessRequest(Uri uri) {
            using (var writer = new StringWriter())
            {
                var hwr =
                    new SimpleWorkerRequest(uri.AbsoluteUri, uri.Query, writer);
                HttpRuntime.ProcessRequest(hwr);
                return writer.ToString();
            }
        }
    }
}
