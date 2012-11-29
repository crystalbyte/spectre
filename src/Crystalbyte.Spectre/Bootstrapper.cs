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
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public abstract class Bootstrapper {
        protected virtual RendererDelegate CreateAppDelegate() {
            return new RendererDelegate();
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

        protected virtual void OnCommandLineProcessing(object sender, CommandLineProcessingEventArgs e)
        {
			var program = Assembly.GetEntryAssembly().Location;
			var codebase = new FileInfo(program).DirectoryName;

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

        protected virtual IList<ScriptingCommand> RegisterScriptingCommands() {
            return new List<ScriptingCommand>();
        }

        private void OnFrameworkInitialized(object sender, EventArgs e) {
            var extensions = RegisterScriptingCommands();
            if (extensions != null) {
                extensions.ForEach(RegisterScriptingCommand);
            }
        }

        private static void RegisterScriptingCommand(ScriptingCommand extension) {
            var name = Guid.NewGuid().ToString();
            ScriptingRuntime.RegisterCommand(name, extension);
        }
    }
}
