#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
