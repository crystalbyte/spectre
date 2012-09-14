using Crystalbyte.Chocolate.Models;
using Crystalbyte.Chocolate.Razor;
using Crystalbyte.Chocolate.Routing;

namespace Crystalbyte.Chocolate.Controllers
{
    [Route("chocolate://localhost/desktop")]
    public sealed class DesktopController : ViewController
    {
        public override IView CreateView() {
            return new RazorView("chocolate://siteoforigin:,,,/Views/desktop.cshtml", new DesktopModel());
        }
    }
}
