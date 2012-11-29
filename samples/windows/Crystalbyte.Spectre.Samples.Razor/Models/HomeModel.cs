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
