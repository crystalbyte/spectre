using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Cherry
{
    public sealed class CherryResourceHandler : ResourceHandler
    {
        protected override void OnResponseDataRequested(ResponseDataRequestingEventArgs e)
        {
            base.OnResponseDataRequested(e);
        }

        protected override void OnResourceRequested(ResourceRequestedEventArgs e)
        {
            base.OnResourceRequested(e);
        }

        protected override void OnCanceled(EventArgs e)
        {
            base.OnCanceled(e);
        }
    }
}
