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
using System.IO;
using Crystalbyte.Spectre.Razor.Templates;

#endregion

namespace Crystalbyte.Spectre.Razor.Hosting.Containers {
    /// <summary>
    ///   Based Host implementation for hosting the RazorHost. This base
    ///   acts as a wrapper for implementing high level host services around
    ///   the RazorHost class. For example implementations can provide assembly
    ///   template caching so assemblies don't recompile for each access.
    /// </summary>
    /// <typeparam name="TBaseTemplateType"> The RazorTemplateBase class that templates will be based on </typeparam>
    public abstract class RazorBaseHostContainer<TBaseTemplateType> : MarshalByRefObject
        where TBaseTemplateType : RazorTemplateBase, new() {
        protected RazorBaseHostContainer() {
            UseAppDomain = true;
            GeneratedNamespace = "__RazorHost";

            ReferencedAssemblies = new List<string>();
            ReferencedNamespaces = new List<string>();

            Configuration = new RazorEngineConfiguration();
        }

        /// <summary>
        ///   Determines whether the Container hosts Razor
        ///   in a separate AppDomain. Seperate AppDomain 
        ///   hosting allows unloading and releasing of 
        ///   resources.
        /// </summary>
        public bool UseAppDomain { get; set; }

        /// <summary>
        ///   Base folder location where the AppDomain 
        ///   is hosted. By default uses the same folder
        ///   as the host application.
        /// 
        ///   Determines where binary dependencies are
        ///   found for assembly references.
        /// </summary>
        public string BaseBinaryFolder { get; set; }

        /// <summary>
        ///   List of referenced assemblies as string values.
        ///   Must be in GAC or in the current folder of the host app/
        ///   base BinaryFolder
        /// </summary>
        public List<string> ReferencedAssemblies { get; set; }

        /// <summary>
        ///   List of additional namespaces to add to all templates.
        /// 
        ///   By default:
        ///   System, System.Text, System.IO, System.Linq
        /// </summary>
        public List<string> ReferencedNamespaces { get; set; }

        /// <summary>
        ///   Name of the generated namespace for template classes
        /// </summary>
        public string GeneratedNamespace { get; set; }

        /// <summary>
        ///   Any error messages
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///   Cached instance of the Host. Required to keep the
        ///   reference to the host alive for multiple uses.
        /// </summary>
        public RazorEngine<TBaseTemplateType> Engine;

        /// <summary>
        ///   Cached instance of the Host Factory - so we can unload
        ///   the host and its associated AppDomain.
        /// </summary>
        protected RazorEngineFactory<TBaseTemplateType> EngineFactory;

        /// <summary>
        ///   Keep track of each compiled assembly
        ///   and when it was compiled.
        /// 
        ///   Use a hash of the string to identify string
        ///   changes.
        /// </summary>
        protected Dictionary<int, CompiledAssemblyItem> LoadedAssemblies = new Dictionary<int, CompiledAssemblyItem>();

        /// <summary>
        ///   Engine Configuration
        /// </summary>
        public RazorEngineConfiguration Configuration { get; set; }

        /// <summary>
        ///   Call to start the Host running. Follow by a calls to RenderTemplate to 
        ///   render individual templates. Call Stop when done.
        /// </summary>
        /// <returns> true or false - check ErrorMessage on false </returns>
        public virtual bool Start() {
            if (Engine == null) {
                if (UseAppDomain)
                    Engine = RazorEngineFactory<TBaseTemplateType>.CreateRazorHostInAppDomain();
                else
                    Engine = RazorEngineFactory<TBaseTemplateType>.CreateRazorHost();

                if (Engine == null)
                    return false;

                Engine.HostContainer = this;

                Engine.ReferencedNamespaces.AddRange(ReferencedNamespaces);

                Engine.Configuration = Configuration;

                if (Engine == null) {
                    ErrorMessage = EngineFactory.ErrorMessage;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///   Stops the Host and releases the host AppDomain and cached
        ///   assemblies.
        /// </summary>
        /// <returns> true or false </returns>
        public bool Stop() {
            LoadedAssemblies.Clear();

            RazorEngineFactory<RazorTemplateBase>.UnloadRazorHostInAppDomain();

            Engine = null;
            return true;
        }

        /// <summary>
        ///   Stock implementation of RenderTemplate that doesn't allow for 
        ///   any sort of assembly caching. Instead it creates and re-renders
        ///   templates read from the reader each time.
        /// 
        ///   Custom implementations of RenderTemplate should be created that
        ///   allow for caching by examing a filename or string hash to determine
        ///   whether a template needs to be re-generated and compiled before
        ///   rendering.
        /// </summary>
        /// <param name="reader"> TextReader that points at the template to compile </param>
        /// <param name="context"> Optional context to pass to template </param>
        /// <param name="writer"> TextReader passed in that receives output </param>
        /// <returns> </returns>
        public bool RenderTemplate(TextReader reader, object context, TextWriter writer) {
            var assemblyId = Engine.ParseAndCompileTemplate(ReferencedAssemblies.ToArray(), reader);

            if (assemblyId == null) {
                ErrorMessage = Engine.ErrorMessage;
                return false;
            }

            return RenderTemplateFromAssembly(assemblyId, context, writer);
        }

        /// <summary>
        ///   Renders a template based on a previously compiled assembly reference. This method allows for
        ///   caching assemblies by their assembly Id.
        /// </summary>
        /// <param name="assemblyId"> Id of a previously compiled assembly </param>
        /// <param name="context"> Optional context object </param>
        /// <param name="writer"> Output writer </param>
        /// <param name="nameSpace"> Namespace for compiled template to execute </param>
        /// <param name="className"> Class name for compiled template to execute </param>
        /// <returns> </returns>
        protected bool RenderTemplateFromAssembly(string assemblyId, object context, TextWriter writer) {
            // String result will be empty as output will be rendered into the
            // Response object's stream output. However a null result denotes
            // an error 
            var result = Engine.RenderTemplateFromAssembly(assemblyId, context, writer);

            if (result == null) {
                ErrorMessage = Engine.ErrorMessage;
                return false;
            }

            return true;
        }

        /// <summary>
        ///   Returns a unique ClassName for a template to execute
        ///   Optionally pass in an objectId on which the code is based
        ///   or null to get default behavior.
        /// 
        ///   Default implementation just returns Guid as string
        /// </summary>
        /// <param name="objectId"> </param>
        /// <returns> </returns>
        protected virtual string GetSafeClassName(object objectId) {
            return "_" + Guid.NewGuid().ToString().Replace("-", "_");
        }

        /// <summary>
        ///   Force this host to stay alive indefinitely
        /// </summary>
        /// <returns> </returns>
        public override object InitializeLifetimeService() {
            return null;
        }

        /// <summary>
        ///   Sets an error message consistently
        /// </summary>
        /// <param name="message"> </param>
        protected void SetError(string message) {
            if (message == null)
                ErrorMessage = string.Empty;

            ErrorMessage = message;
        }

        /// <summary>
        ///   Sets an error message consistently
        /// </summary>
        protected void SetError() {
            SetError(null);
        }
        }
}
