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
using System.Reflection;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public abstract class Bootstrapper {
        protected virtual AppDelegate CreateAppDelegate() {
            return new AppDelegate();
        }

        protected abstract IEnumerable<Viewport> CreateViewports();

        public virtual void Run() {
            var app = CreateAppDelegate();
            app.CustomSchemesRegistering += OnCustomSchemesRegistering;
            app.Initialized += OnFrameworkInitialized;

            ConfigureSettings(Application.Current.Settings);

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

        protected virtual void OnStarting(object sender, EventArgs e) {
            // override
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e) {
            var descriptors = RegisterSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(ApplicationSettings settings) {
            if (Platform.IsLinux || Platform.IsOsX) {
                var fullname = Assembly.GetEntryAssembly().Location;
                settings.BrowserSubprocessPath = string.Format("/usr/bin/mono \"{0}\"", fullname);
            }

            var culture = CultureInfo.CurrentCulture.Name;
            settings.Locale = culture != string.Empty ? culture : "en-US";

            var modulePath = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            settings.LocalesDirPath = Path.Combine(modulePath, "locales");

#if DEBUG
            settings.LogSeverity = LogSeverity.LogseverityVerbose;
#else
            settings.LogSeverity = LogSeverity.LogseverityInfo;
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
