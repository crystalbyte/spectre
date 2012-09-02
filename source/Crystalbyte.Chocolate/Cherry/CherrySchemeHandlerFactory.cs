using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Cherry
{
    public sealed class CherrySchemeHandlerFactory : SchemeHandlerFactory
    {
        protected override ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e)
        {
            return new CherryResourceHandler();
        }
    }
}
