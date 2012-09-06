using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    public abstract class ViewController {
        public abstract IView CreateView();
    }
}
