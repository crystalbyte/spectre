using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Mvc;
using Crystalbyte.Chocolate.Razor;
using Crystalbyte.Chocolate.Models;

namespace Crystalbyte.Chocolate.Controllers
{
    [Route("/views/demo/windows")]
    public sealed class DemoController : ViewController
    {
        public override IView CreateView() {
            return new RazorView("Views/Windows.cshtml", new DemoModel());
        }
    }
}
