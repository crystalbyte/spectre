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

#endregion

namespace Crystalbyte.Spectre.Razor.Hosting {
    /// <summary>
    ///   Helper class that provides a few simple utilitity functions to the project
    /// </summary>
    public class Utilities {
        /// <summary>
        ///   Returns a relative path based on a base path.
        /// 
        ///   Examples:
        ///   &lt;&lt;ul&gt;&gt;
        ///   &lt;&lt;li&gt;&gt; filename.txt
        ///   &lt;&lt;li&gt;&gt; subDir\filename.txt
        ///   &lt;&lt;li&gt;&gt; ..\filename.txt
        ///   &lt;&lt;li&gt;&gt; ..\..\filename.txt
        ///   &lt;&lt;/ul&gt;&gt;
        ///   <seealso>Class Utilities</seealso>
        /// </summary>
        /// <param name="fullPath"> The full path from which to generate a relative path </param>
        /// <param name="basePath"> The base path based on which the relative path is based on </param>
        /// <returns> string </returns>
        public static string GetRelativePath(string fullPath, string basePath) {
            // ForceBasePath to a path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            var baseUri = new Uri(basePath);
            var fullUri = new Uri(fullPath);

            var relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slahes
            return relativeUri.ToString().Replace("/", "\\");
        }
    }
}
