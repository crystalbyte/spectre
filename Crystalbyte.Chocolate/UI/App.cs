using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class App : CountedAdapter
    {
        public App() 
            : base(typeof(CefApp)) {
            MarshalToNative(new CefApp {
                Base = DedicatedBase,
            });
        }
    }
}
