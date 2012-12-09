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

#endregion

namespace Crystalbyte.Spectre.Samples.Models {
    [Serializable]
    public sealed class HomeModel {
        public HomeModel() {
            Styles = new List<string> {"../Styles/index.css"};
            Scripts = new List<string> {"../Scripts/jquery-1.8.2.min.js"};

            Header = "Razor";
            Description =
                "Web Pages with Razor syntax provide an alternative "
                + "to ASP.NET Web Forms. Web Forms pages center around "
                + "Web server controls that generate HTML automatically "
                + "and that emulate the event-based programming model "
                + "used for client applications. In contrast, Razor pages "
                + "work more directly like standard HTML pages, "
                + "where you create virtually all of the HTML markup "
                + "yourself and where you then add functionality around "
                + "that markup using server code. In general, "
                + "Razor pages are more lightweight than Web Forms "
                + "pages. Because of that and because the syntax is simple"
                + ", Razor can be easier for programmers to learn and "
                + "can be faster for developing dynamic Web pages.";

            CreationDate = DateTime.Now;
        }

        public IEnumerable<string> Styles { get; set; }
        public IEnumerable<string> Scripts { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
