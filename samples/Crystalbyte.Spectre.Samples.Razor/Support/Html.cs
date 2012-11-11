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

namespace Crystalbyte.Spectre.Samples.Support {
    public static class Html {
        public static string Image(string source, string alt) {
            return string.Format("<img src=\"{0}\" alt=\"{1}\" />", source, alt);
        }

        public static string Header(string text) {
            return string.Format("<h1>{0}</h1>", text);
        }

        public static string Span(string text) {
            return string.Format("<span>{0}</span>", text);
        }
    }
}
