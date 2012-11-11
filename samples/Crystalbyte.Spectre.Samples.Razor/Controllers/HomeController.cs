#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System.Collections.Generic;
using Crystalbyte.Spectre.Razor;
using Crystalbyte.Spectre.Samples.Models;

#endregion

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
