using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Mvc;

namespace Crystalbyte.Chocolate.Controllers
{
    [Route("/views/demo/windows")]
    public sealed class DemoController : ViewController
    {
        public override IView CreateView() {
            return new RazorView();
        }
    }
}
