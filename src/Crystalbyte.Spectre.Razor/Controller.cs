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

using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre.Razor {
    public abstract class Controller {
        protected Controller() {
            _list = new List<string> {
                "Crystalbyte.Spectre.Razor.dll"
            };
        }

        public void Initialize(Request request) {
            if (request == null)
                throw new ArgumentNullException("request");

            Request = request;
        }

        protected Request Request { get; private set; }

        public abstract ActionResult Execute();

        protected ViewResult View(object model) {
            return new ViewResult(new RazorView(model));
        }

        protected RedirectResult Redirect(string url) {
            return new RedirectResult(url);
        }

        private readonly IList<string> _list;

        public virtual IList<string> ConfigureReferencedAssemblies() {
            return _list;
        }
    }
}
