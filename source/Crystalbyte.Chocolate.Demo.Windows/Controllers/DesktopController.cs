using Crystalbyte.Chocolate.IO;
using Crystalbyte.Chocolate.Models;
using Crystalbyte.Chocolate.Razor;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Controllers
{
    public sealed class DesktopController : ViewController
    {
        public override IView CreateView() {
            return new RazorView("pack://siteoforigin:,,,/Views/Desktop.cshtml", new DesktopModel());
        }
    }
}
