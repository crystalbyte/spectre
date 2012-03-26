using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    public sealed class Browser : Adapter
    {
        public Browser() : base(typeof(CefBrowser), true) {
            
        }
    }
}
