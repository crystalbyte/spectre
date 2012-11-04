using Crystalbyte.Spectre.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Spectre.Samples.Models;

namespace Crystalbyte.Spectre.Samples.Controllers {
    public sealed class HomeController : Controller {
        public override IList<string> ConfigureReferencedAssemblies() {
            var assemblies = base.ConfigureReferencedAssemblies();
            assemblies.Add("Crystalbyte.Spectre.Samples.Razor.exe");
            return assemblies;
        }

        public override ActionResult Execute() {
            return View(new HomeModel());
        }
    }
}
