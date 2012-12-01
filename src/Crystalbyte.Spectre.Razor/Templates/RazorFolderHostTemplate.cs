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

using Crystalbyte.Spectre.Razor.Hosting.Containers;

#endregion

namespace Crystalbyte.Spectre.Razor.Templates {
    /// <summary>
    ///   Custom template implementation for the FolderHostContainer that supports 
    ///   relative path based partial rendering.
    /// </summary>
    public class RazorFolderHostTemplate : RazorTemplateBase {
        public override void InitializeTemplate(object context, object configurationData) {
            // Pick up configuration data and stuff into Request object
            var config = configurationData as RazorFolderHostTemplateConfiguration;

            Request.TemplatePath = config.TemplatePath;
            Request.TemplateRelativePath = config.TemplateRelativePath;
        }

        /// <summary>
        ///   Render a partial view based on a Web relative path
        /// </summary>
        /// <param name="relativePath"> </param>
        /// <param name="context"> </param>
        /// <returns> </returns>
        public string RenderPartial(string relativePath, object context) {
            if (HostContainer == null)
                return null;

            // we don't know the exact type since it can be generic so make dynamic
            // execution possible with dynamic type
            dynamic hostContainer = HostContainer;

            // now execute the child request to a string
            string output = hostContainer.RenderTemplateToString(relativePath, context);

            if (output == null)
                output = "!@ Error: " + hostContainer.ErrorMessage + " @!";

            return output;
        }

#if false
    /// TODO:
    /// this isn't working at this point because RenderTemplate in the host
    /// and engine will release the response. 
    /// Will need custom overloads that keep the writer open through child
    /// rendering operations.

    ///// <summary>
    ///// Render a partial view directly into the response object
    ///// </summary>
    ///// <param name="relativePath"></param>
    ///// <param name="context"></param>
    //public string RenderPartialResponse(string relativePath, object context)
    //{
    //    if (this.HostContainer == null)
    //        return null;

    //    RazorFolderHostContainer hostContainer = (RazorFolderHostContainer)HostContainer; 
    //    hostContainer.RenderTemplate(relativePath, context, Response.Writer, true);

    //    return null;
    //}
#endif
    }
}
