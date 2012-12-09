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
