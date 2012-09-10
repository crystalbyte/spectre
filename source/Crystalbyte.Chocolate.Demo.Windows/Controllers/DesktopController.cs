using Crystalbyte.Chocolate.Mvc;
using Crystalbyte.Chocolate.Models;
using Crystalbyte.Chocolate.Razor;

namespace Crystalbyte.Chocolate.Controllers
{
    [Route("/windows")]
    public sealed class DesktopController : ViewController
    {
        public override IView CreateView() {
            return new RazorView("Views/desktop.cshtml", new DesktopModel());
        }
    }
}
