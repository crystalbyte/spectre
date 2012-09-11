using Crystalbyte.Chocolate.Mvc;
using Crystalbyte.Chocolate.Models;
using Crystalbyte.Chocolate.Razor;

namespace Crystalbyte.Chocolate.Controllers
{
    [Route("mvc://localhost/start")]
    public sealed class DesktopController : ViewController
    {
        public override IView CreateView() {
            return new RazorView("pack://siteoforigin:,,,/Views/desktop.cshtml", new DesktopModel());
        }
    }
}
