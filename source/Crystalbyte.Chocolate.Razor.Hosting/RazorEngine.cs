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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Razor;
using Microsoft.CSharp;

#endregion

namespace Crystalbyte.Chocolate.Razor {
    /// <summary>
    ///   The main hosting class that handles instantiation of the
    ///   host and rendering, compiling and executing of templates.
    /// </summary>
    /// <typeparam name="TBaseTemplateType"> RazorTemplateHost based type </typeparam>
    public class RazorEngine<TBaseTemplateType> : MarshalByRefObject
        where TBaseTemplateType : RazorTemplateBase {
        /// <summary>
        ///   Any errors that occurred during template execution
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///   Last generated output
        /// </summary>
        public string LastGeneratedCode { get; set; }

        /// <summary>
        ///   Holds Razor Configuration Properties
        /// </summary>
        public RazorEngineConfiguration Configuration { get; set; }

        /// <summary>
        ///   Provide a reference to a RazorHost container so that it
        ///   can be passed to a template.
        /// 
        ///   This may be null, but if a container is available this value
        ///   is set and passed on to the template as HostContainer.
        /// </summary>
        public object HostContainer { get; set; }

        /// <summary>
        ///   A list of default namespaces to include
        /// 
        ///   Defaults already included:
        ///   System, System.Text, System.IO, System.Collections.Generic, System.Linq
        /// </summary>
        public List<string> ReferencedNamespaces { get; set; }


        /// <summary>
        ///   Internally cache assemblies loaded with ParseAndCompileTemplate.        
        ///   Assemblies are cached in the EngineHost so they don't have
        ///   to cross AppDomains for invocation when running in a separate AppDomain
        /// </summary>
        protected Dictionary<string, Assembly> AssemblyCache { get; set; }


        /// <summary>
        ///   A property that holds any per request configuration 
        ///   data that is to be passed to the template. This object
        ///   is passed to InitializeTemplate after the instance was
        ///   created.
        /// 
        ///   This object must be serializable. 
        ///   This object should be set on every request and cleared out after 
        ///   each request
        /// </summary>
        public object TemplatePerRequestConfigurationData { get; set; }

        public RazorEngine() {
            Configuration = new RazorEngineConfiguration();
            AssemblyCache = new Dictionary<string, Assembly>();
            ErrorMessage = string.Empty;

            ReferencedNamespaces = new List<string>();
            ReferencedNamespaces.Add("System");
            ReferencedNamespaces.Add("System.Text");
            ReferencedNamespaces.Add("System.Collections.Generic");
            ReferencedNamespaces.Add("System.Linq");
            ReferencedNamespaces.Add("System.IO");
        }


        /// <summary>
        ///   Creates an instance of the RazorHost with various options applied.
        ///   Applies basic namespace imports and the name of the class to generate
        /// </summary>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <returns> </returns>
        protected RazorTemplateEngine CreateHost(string generatedNamespace, string generatedClass) {
            var baseClassType = typeof (TBaseTemplateType);

            var host = new RazorEngineHost(new CSharpRazorCodeLanguage());
            host.DefaultBaseClass = baseClassType.FullName;
            host.DefaultClassName = generatedClass;
            host.DefaultNamespace = generatedNamespace;

            foreach (var ns in ReferencedNamespaces) {
                host.NamespaceImports.Add(ns);
            }

            return new RazorTemplateEngine(host);
        }


        /// <summary>
        ///   Parses and compiles a markup template into an assembly and returns
        ///   an assembly name. The name is an ID that can be passed to 
        ///   ExecuteTemplateByAssembly which picks up a cached instance of the
        ///   loaded assembly.
        /// </summary>
        /// <param name="ReferencedAssemblies"> Any referenced assemblies by dll name only. Assemblies must be in execution path of host or in GAC. </param>
        /// <param name="templateSourceReader"> Textreader that loads the template </param>
        /// <param name="generatedNamespace"> The namespace of the class to generate from the template. null generates name. </param>
        /// <param name="generatedClassName"> The name of the class to generate from the template. null generates name. </param>
        /// <remarks>
        ///   The actual assembly isn't returned here to allow for cross-AppDomain
        ///   operation. If the assembly was returned it would fail for cross-AppDomain
        ///   calls.
        /// </remarks>
        /// <returns> An assembly Id. The Assembly is cached in memory and can be used with RenderFromAssembly. </returns>
        public string ParseAndCompileTemplate(
            string[] ReferencedAssemblies,
            TextReader templateSourceReader,
            string generatedNamespace,
            string generatedClassName) {
            if (string.IsNullOrEmpty(generatedNamespace)) {
                generatedNamespace = "__RazorHost";
            }
            if (string.IsNullOrEmpty(generatedClassName)) {
                generatedClassName = GetSafeClassName(null);
            }

            var engine = CreateHost(generatedNamespace, generatedClassName);

            // Generate the template class as CodeDom  
            var razorResults = engine.GenerateCode(templateSourceReader);

            // Create code from the codeDom and compile
            var codeProvider = new CSharpCodeProvider();
            var options = new CodeGeneratorOptions();

            // Capture Code Generated as a string for error info
            // and debugging
            LastGeneratedCode = null;
            using (var writer = new StringWriter()) {
                codeProvider.GenerateCodeFromCompileUnit(razorResults.GeneratedCode, writer, options);
                LastGeneratedCode = writer.ToString();
            }

            var compilerParameters = new CompilerParameters(ReferencedAssemblies);

            // Standard Assembly References
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            compilerParameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            // dynamic support!                     

            // Also add the current assembly so RazorTemplateBase is available
            compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().CodeBase.Substring(8));

            compilerParameters.GenerateInMemory = Configuration.CompileToMemory;
            if (!Configuration.CompileToMemory) {
                compilerParameters.OutputAssembly = Path.Combine(Configuration.TempAssemblyPath,
                                                                 "_" + Guid.NewGuid().ToString("n") + ".dll");
            }

            var compilerResults = codeProvider.CompileAssemblyFromDom(compilerParameters, razorResults.GeneratedCode);
            if (compilerResults.Errors.Count > 0) {
                var compileErrors = new StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError compileError in compilerResults.Errors) {
                    compileErrors.Append(String.Format("Line: {0}\t Col: {1}\t Error: {2}", compileError.Line,
                                                       compileError.Column, compileError.ErrorText));
                }

                SetError(compileErrors + "\r\n" + LastGeneratedCode);
                return null;
            }

            AssemblyCache.Add(compilerResults.CompiledAssembly.FullName, compilerResults.CompiledAssembly);

            return compilerResults.CompiledAssembly.FullName;
        }

        /// <summary>
        ///   Parses and compiles a markup template into an assembly and returns
        ///   an assembly name. The name is an ID that can be passed to 
        ///   ExecuteTemplateByAssembly which picks up a cached instance of the
        ///   loaded assembly.
        /// </summary>
        /// <param name="ReferencedAssemblies"> Any referenced assemblies by dll name only. Assemblies must be in execution path of host or in GAC. </param>
        /// <param name="templateSourceReader"> Textreader that loads the template </param>
        /// <remarks>
        ///   The actual assembly isn't returned here to allow for cross-AppDomain
        ///   operation. If the assembly was returned it would fail for cross-AppDomain
        ///   calls.
        /// </remarks>
        /// <returns> An assembly Id. The Assembly is cached in memory and can be used with RenderFromAssembly. </returns>
        public string ParseAndCompileTemplate(
            string[] ReferencedAssemblies,
            TextReader templateSourceReader) {
            return ParseAndCompileTemplate(ReferencedAssemblies, templateSourceReader, null, null);
        }

        /// <summary>
        ///   Executes a template based on a previously generated assembly reference.
        ///   This effectively allows you to cache an assembly.
        /// </summary>
        /// <param name="generatedAssemblyId"> </param>
        /// <param name="context"> </param>
        /// <param name="outputWriter"> A text writer that receives output generated by the template. Writer is closed after rendering. </param>
        /// <returns> if no outputWriter is passed the response is returned as a string or null on error. if an outputWriter is passed the response is an empty string on success or null on error </returns>
        public string RenderTemplateFromAssembly(
            string assemblyId,
            object context,
            TextWriter outputWriter) {
            return RenderTemplateFromAssembly(assemblyId, context, outputWriter, null, null);
        }


        /// <summary>
        ///   Executes a template based on a previously generated assembly reference.
        ///   This effectively allows you to cache an assembly.
        /// </summary>
        /// <param name="generatedAssembly"> </param>
        /// <param name="context"> </param>
        /// <param name="outputWriter"> A text writer that receives output generated by the template. Writer is closed after rendering. </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <returns> if no outputWriter is passed the response is returned as a string or null on error. if an outputWriter is passed the response is an empty string on success or null on error </returns>
        public string RenderTemplateFromAssembly(
            string assemblyId,
            object context,
            TextWriter outputWriter,
            string generatedNamespace,
            string generatedClass) {
            SetError();

            var generatedAssembly = AssemblyCache[assemblyId];
            if (generatedAssembly == null) {
                SetError("Previously compiled assembly not found.");
                return null;
            }

            // find the generated type to instantiate
            Type type = null;

            if (string.IsNullOrEmpty(generatedNamespace) || string.IsNullOrEmpty(generatedClass)) {
                var types = generatedAssembly.GetTypes();

                // Generated assembly only contains one type
                if (types.Length > 0) {
                    type = types[0];
                }

                if (type == null) {
                    SetError("Unable to create type.");
                    return null;
                }
            }
            else {
                var className = generatedNamespace + "." + generatedClass;
                try {
                    type = generatedAssembly.GetType(className);
                }
                catch (Exception ex) {
                    SetError("Unable to create type " + className + ": " + ex.Message);
                    return null;
                }
            }

            // Start with empty non-error response (if we use a writer)
            var result = string.Empty;

            using (var instance = InstantiateTemplateClass(type)) {
                if (TemplatePerRequestConfigurationData != null) {
                    instance.InitializeTemplate(context, TemplatePerRequestConfigurationData);
                }

                if (instance == null) {
                    return null;
                }

                if (outputWriter != null) {
                    instance.Response.SetTextWriter(outputWriter);
                }

                if (!InvokeTemplateInstance(instance, context)) {
                    return null;
                }

                // Capture string output if implemented and return
                // otherwise null is returned
                if (outputWriter == null) {
                    result = instance.Response.ToString();
                }
            }

            return result;
        }

        /// <summary>
        ///   Executes a template based on a previously generated assembly reference.
        ///   This effectively allows you to cache an assembly.
        /// </summary>
        /// <param name="generatedAssembly"> </param>
        /// <param name="context"> </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <returns> The response is returned as a string or null on error. </returns>
        public string RenderTemplateFromAssembly(
            string assemblyId,
            object context,
            string generatedNamespace,
            string generatedClass) {
            return RenderTemplateFromAssembly(assemblyId, context, null, generatedNamespace, generatedClass);
        }


        /// <summary>
        ///   Executes a template based on a previously generated assembly reference.
        ///   This effectively allows you to cache an assembly.
        /// </summary>
        /// <param name="generatedAssembly"> </param>
        /// <param name="context"> </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <returns> The response is returned as a string or null on error. </returns>
        public string RenderTemplateFromAssembly(
            string assemblyId,
            object context) {
            return RenderTemplateFromAssembly(assemblyId, context, null, null, null);
        }


        /// <summary>
        ///   Execute a template based on a TextReader input into a provided TextWriter object.
        /// </summary>
        /// <param name="mplateSourceReader"> A text reader that reads in the markup template </param>
        /// <param name="generatedNamespace"> Name of the namespace that is generated </param>
        /// <param name="generatedClass"> Name of the class that is generated </param>
        /// <param name="referencedAssemblies"> Any assembly references required by template as a DLL names. Must be in execution path or GAC. </param>
        /// <param name="context"> Optional context available in the template as this.Context </param>
        /// <param name="outputWriter"> A text writer that receives the rendered template output. Writer is closed after rendering. </param>
        /// <returns> </returns>
        public string RenderTemplate(
            TextReader templateSourceReader,
            string[] referencedAssemblies,
            object context,
            TextWriter outputWriter) {
            SetError();

            var assemblyId = ParseAndCompileTemplate(referencedAssemblies, templateSourceReader);

            if (assemblyId == null) {
                return null;
            }

            return RenderTemplateFromAssembly(assemblyId, context, outputWriter);
        }

        /// <summary>
        ///   Executes a template from a text reader to a string
        /// </summary>
        /// <param name="sourceCodeReader"> TextReader that points at the template markup code. </param>
        /// <param name="generatedNamespace"> Name of the namespace that is generated. </param>
        /// <param name="generatedClass"> Name of the class that is generated. </param>
        /// <param name="referencedAssemblies"> Referenced assemblies as dll names. must be in execution path of host application. </param>
        /// <param name="context"> Optional context to pass to template (as this.Template) </param>
        /// <returns> </returns>
        public string RenderTemplate(
            TextReader sourceCodeReader,
            string[] referencedAssemblies,
            object context) {
            return RenderTemplate(sourceCodeReader, referencedAssemblies, context, null);
        }


        /// <summary>
        ///   Executes a template based on string input and generates a string result.
        /// </summary>
        /// <param name="inputTemplateText"> </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <param name="referencedAssemblies"> </param>
        /// <param name="context"> </param>
        /// <returns> </returns>
        public string RenderTemplate(
            string inputTemplateText,
            string[] referencedAssemblies,
            object context) {
            TextReader sourceCodeReader = new StringReader(inputTemplateText);
            return RenderTemplate(sourceCodeReader,
                                  referencedAssemblies, context, null);
        }

        /// <summary>
        ///   Executes a template to file from a string input
        /// </summary>
        /// <param name="inputTemplateText"> </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <param name="referencedAssemblies"> </param>
        /// <param name="context"> </param>
        /// <returns> </returns>
        public bool RenderTemplateToFile(
            string inputTemplateText,
            string[] referencedAssemblies,
            object context,
            string outputFile) {
            string result = null;

            using (TextReader sourceCodeReader = new StringReader(inputTemplateText)) {
                using (
                    var writer = new StreamWriter(outputFile, false, Configuration.OutputEncoding,
                                                  Configuration.StreamBufferSize)) {
                    result = RenderTemplate(sourceCodeReader,
                                            referencedAssemblies, context, writer);
                }
            }
            return result != null ? true : false;
        }


        /// <summary>
        ///   Executes a template to file from a string input
        /// </summary>
        /// <param name="inputTemplateText"> </param>
        /// <param name="generatedNamespace"> </param>
        /// <param name="generatedClass"> </param>
        /// <param name="referencedAssemblies"> </param>
        /// <param name="context"> </param>
        /// <returns> </returns>
        public bool RenderTemplateToFile(
            TextReader sourceCodeReader,
            string[] referencedAssemblies,
            object context,
            string outputFile) {
            string result = null;
            using (
                var writer = new StreamWriter(outputFile, false, Configuration.OutputEncoding,
                                              Configuration.StreamBufferSize)) {
                result = RenderTemplate(sourceCodeReader,
                                        referencedAssemblies, context, writer);
            }
            return result != null ? true : false;
        }


        /// <summary>
        ///   Allows retrieval of an Assembly cached internally by its id
        ///   returned from ParseAndCompileTemplate. Useful if you want
        ///   to write an assembly to disk for later activation
        /// </summary>
        /// <param name="assemblyId"> </param>
        public Assembly GetAssemblyFromId(string assemblyId) {
            Assembly ass = null;
            AssemblyCache.TryGetValue(assemblyId, out ass);
            return ass;
        }


        /// <summary>
        ///   Overridable instance creation routine for the host. 
        /// 
        ///   Handle custom template base classes (derived from RazorTemplateBase)
        ///   and setting of properties on the instance in subclasses by overriding
        ///   this method.
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> </returns>
        protected virtual TBaseTemplateType InstantiateTemplateClass(Type type) {
            var instance = Activator.CreateInstance(type) as TBaseTemplateType;

            if (instance == null) {
                SetError("Couldn't activate type instance: " + type.FullName);
                return null;
            }

            instance.Engine = this;

            // If a HostContainer was set pass that to the template too
            instance.HostContainer = HostContainer;

            return instance;
        }

        /// <summary>
        ///   Internally executes an instance of the template,
        ///   captures errors on execution and returns true or false
        /// </summary>
        /// <param name="instance"> An instance of the generated template </param>
        /// <returns> true or false - check ErrorMessage for errors </returns>
        protected virtual bool InvokeTemplateInstance(TBaseTemplateType instance, object context) {
            try {
                instance.Context = context;
                instance.Execute();
            }
            catch (Exception ex) {
                SetError("Template Execution Error: " + ex.Message);
                return false;
            }
            finally {
                // Must make sure Response is closed
                instance.Response.Dispose();
            }
            return true;
        }

        /// <summary>
        ///   Override to allow indefinite lifetime - no unloading
        /// </summary>
        /// <returns> </returns>
        public override object InitializeLifetimeService() {
            return null;
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
        ///   Sets error information consistently
        /// </summary>
        /// <param name="message"> </param>
        public void SetError(string message) {
            if (message == null) {
                ErrorMessage = string.Empty;
            }
            else {
                ErrorMessage = message;
            }
        }

        public void SetError() {
            SetError(null);
        }
        }
}