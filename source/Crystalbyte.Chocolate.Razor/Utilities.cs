#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;

#endregion

namespace Crystalbyte.Chocolate.Razor {
    /// <summary>
    ///   Helper class that provides a few simple utilitity functions to the project
    /// </summary>
    internal static class Utilities {
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
            if (!basePath.EndsWith("\\")) {
                basePath += "\\";
            }

            var baseUri = new Uri(basePath);
            var fullUri = new Uri(fullPath);

            var relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slahes
            return relativeUri.ToString().Replace("/", "\\");
        }
    }
}