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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public abstract class Bootstrapper {
        protected virtual RenderDelegate CreateAppDelegate() {
            return new RenderDelegate();
        }

        protected abstract IEnumerable<Viewport> CreateViewports();

        public virtual void Run() {
            ConfigureSettings(Application.Current.Settings);

            var app = CreateAppDelegate();
            app.CustomSchemesRegistering += OnCustomSchemesRegistering;
            app.Initialized += OnFrameworkInitialized;
            app.CommandLineProcessing += OnCommandLineProcessing;
            // Initialize will block sub processes, only the host process will continue.
            Application.Current.Initialize(app);

            if (!Application.Current.IsRootProcess) {
                // Any sub process will terminate at this point
                return;
            }

            // Only the browser process will run the code below
            var factories = RegisterSchemeHandlerFactories();
            factories.ForEach(Application.Current.SchemeFactories.Register);

            var viewports = CreateViewports();
            viewports.ForEach(Application.Current.Add);

            Application.Current.Starting += OnStarting;
            Application.Current.Run();
            Application.Current.Shutdown();
        }

        protected virtual void OnCommandLineProcessing(object sender, CommandLineProcessingEventArgs e) {
            var program = Assembly.GetEntryAssembly().Location;
            var codebase = new FileInfo(program).DirectoryName;
            if (codebase == null) {
                throw new NullReferenceException("codebase must not be null");
            }

            if (!e.CommandLine.HasSwitch("lang")) {
                var locale = !string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name)
                                 ? CultureInfo.CurrentCulture.Name
                                 : "en-US";
                e.CommandLine.AppendSwitchWithValue("lang", locale);
            }

            if (!e.CommandLine.HasSwitch("locales-dir-path")) {
                e.CommandLine.AppendSwitchWithValue("locales-dir-path", Path.Combine(codebase, "locales"));
            }

            if (!e.CommandLine.HasSwitch("resources-dir-path")) {
                e.CommandLine.AppendSwitchWithValue("resources-dir-path", codebase);
            }
        }

        protected virtual void OnStarting(object sender, EventArgs e) {
            // override
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e) {
            var descriptors = RegisterSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(ApplicationSettings settings) {
            var program = Assembly.GetEntryAssembly().Location;

            var isMonoHosted = Type.GetType("Mono.Runtime") != null;
            if (isMonoHosted) {
#if DEBUG

			settings.BrowserSubprocessPath = string.Format("/usr/bin/mono --debug {0}", program);
#else
                settings.BrowserSubprocessPath = string.Format("/usr/bin/mono {0}", program);
#endif
            }

#if DEBUG
			settings.LogSeverity = LogSeverity.LogseverityVerbose;
#endif
        }

        protected virtual IList<ISchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories() {
            return new List<ISchemeHandlerFactoryDescriptor> {
                new SpectreSchemeHandlerFactoryDescriptor()
            };
        }

        protected virtual IList<ISchemeDescriptor> RegisterSchemeHandlers() {
            return new List<ISchemeDescriptor> {
                new SpectreSchemeDescriptor()
            };
        }

        protected virtual IList<Extension> RegisterScriptingExtensions() {
            return new List<Extension>();
        }

        private void OnFrameworkInitialized(object sender, EventArgs e) {
            var extensions = RegisterScriptingExtensions();
            if (extensions != null) {
                extensions.ForEach(RegisterScriptingExtension);
            }
        }

        private static void RegisterScriptingExtension(Extension extension) {
            var name = Guid.NewGuid().ToString();
            ScriptingRuntime.RegisterExtension(name, extension);
        }
    }
}
