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
using System.Collections.Generic;
using Crystalbyte.Chocolate.Scripting;
using Crystalbyte.Chocolate.Web;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public abstract class Bootstrapper {
        protected abstract IRenderTarget CreateRenderTarget();

        protected virtual AppDelegate CreateAppDelegate() {
            return new AppDelegate();
        }

        protected virtual BrowserDelegate CreateBrowserDelegate(IRenderTarget target) {
            return new BrowserDelegate();
        }

        public virtual void Run() {
            var app = CreateAppDelegate();
            app.CustomSchemesRegistering += OnCustomSchemesRegistering;
            app.Initialized += OnFrameworkInitialized;

            ConfigureSettings(Application.Current.Settings);
            Application.Current.Initialize(app);

            if (!Application.Current.IsRootProcess) {
                return;
            }

            var factories = RegisterSchemeHandlerFactories();
            factories.ForEach(Application.Current.SchemeFactories.Register);

            var target = CreateRenderTarget();
            var browserDelegate = CreateBrowserDelegate(target);

            Application.Current.Run(new Viewport(target, browserDelegate));
            Application.Current.Shutdown();
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e) {
            var descriptors = RegisterSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(ApplicationSettings settings) {
#if DEBUG
            settings.LogSeverity = LogSeverity.LogseverityVerbose;
#else
            settings.LogSeverity = LogSeverity.LogseverityInfo;
#endif
        }

        protected virtual IList<ISchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories() {
            return new List<ISchemeHandlerFactoryDescriptor> {
                new ChocolateSchemeHandlerFactoryDescriptor()
            };
        }

        protected virtual IList<ISchemeDescriptor> RegisterSchemeHandlers() {
            return new List<ISchemeDescriptor> {
                new ChocolateSchemeDescriptor()
            };
        }

        protected virtual IList<RuntimeExtension> RegisterScriptingExtensions() {
            return new List<RuntimeExtension>();
        }

        private void OnFrameworkInitialized(object sender, EventArgs e) {
            var extensions = RegisterScriptingExtensions();
            if (extensions != null) {
                extensions.ForEach(RegisterScriptingExtension);
            }
        }

        private static void RegisterScriptingExtension(RuntimeExtension extension) {
            var name = Guid.NewGuid().ToString();
            ScriptingRuntime.RegisterExtension(name, extension);
        }
    }
}