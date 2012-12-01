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
using System.IO;
using Crystalbyte.Spectre.Razor.Templates;

#endregion

namespace Crystalbyte.Spectre.Razor.Hosting.Containers {
    /// <summary>
    ///   This class is a caching directory based host wrapper around
    ///   the RazorHost classes to provide directory based Razor
    ///   template execution. Templates are compiled on 
    ///   the fly, and cached unless the templates on disk are changed.
    /// 
    ///   Runs Razor Templates in a seperate AppDomain
    /// </summary>
    public class RazorStringHostContainer : RazorBaseHostContainer<RazorTemplateBase> {
        public RazorStringHostContainer() {
            BaseBinaryFolder = Environment.CurrentDirectory;
        }

        /// <summary>
        ///   Call this method to actually render a template to the specified outputfile
        /// </summary>
        /// "
        /// <param name="templateText"> The template text to parse and render </param>
        /// <param name="context"> Any object that will be available in the template as a dynamic of this.Context </param>
        /// <returns> true if rendering succeeds, false on failure - check ErrorMessage </returns>
        public string RenderTemplate(string templateText, object context) {
            var assItem = GetAssemblyFromStringAndCache(templateText);
            if (assItem == null)
                return null;

            // String result will be empty as output will be rendered into the
            // Response object's stream output. However a null result denotes
            // an error 
            var result = Engine.RenderTemplateFromAssembly(assItem.AssemblyId, context);

            if (result == null) {
                ErrorMessage = Engine.ErrorMessage;
                return null;
            }

            return result;
        }

        /// <summary>
        ///   Renders a template from a string input to a file output.
        ///   Same text templates are compiled and cached for re-use.
        /// </summary>
        /// <param name="templateText"> Text of the template to run </param>
        /// <param name="context"> Optional context to pass </param>
        /// <param name="outputFile"> Output file where output is sent to </param>
        /// <returns> </returns>
        public bool RenderTemplateToFile(string templateText, object context, string outputFile) {
            var assItem = GetAssemblyFromStringAndCache(templateText);
            if (assItem == null)
                return false;

            StreamWriter writer = null;
            try {
                writer = new StreamWriter(outputFile, false, Engine.Configuration.OutputEncoding,
                                          Engine.Configuration.StreamBufferSize);
            }
            catch (Exception ex) {
                SetError("Unable to write template output to " + outputFile + ": " + ex.Message);
                return false;
            }

            return RenderTemplateFromAssembly(assItem.AssemblyId, context, writer);
        }

        /// <summary>
        ///   Internally tries to retrieve a previously compiled template from cache
        ///   if not found compiles a template into an assembly
        ///   always returns an assembly id as a string.
        /// </summary>
        /// <param name="templateText"> The text to parse </param>
        /// <returns> assembly id as a string or null on error </returns>
        protected virtual CompiledAssemblyItem GetAssemblyFromStringAndCache(string templateText) {
            var hash = templateText.GetHashCode();

            CompiledAssemblyItem item = null;
            LoadedAssemblies.TryGetValue(hash, out item);

            string assemblyId = null;

            // Check for cached instance
            if (item != null)
                assemblyId = item.AssemblyId;
            else
                item = new CompiledAssemblyItem();

            // No cached instance - create assembly and cache
            if (assemblyId == null) {
                var safeClassName = GetSafeClassName(null);
                using (var reader = new StringReader(templateText)) {
                    var refAssemblies = ReferencedAssemblies.ToArray();
                    assemblyId = Engine.ParseAndCompileTemplate(refAssemblies, reader, GeneratedNamespace, safeClassName);
                }

                if (assemblyId == null) {
                    ErrorMessage = Engine.ErrorMessage;
                    return null;
                }

                item.AssemblyId = assemblyId;
                item.CompileTimeUtc = DateTime.UtcNow;
                item.SafeClassName = safeClassName;

                LoadedAssemblies[hash] = item;
            }

            return item;
        }
    }
}
