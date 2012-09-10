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
using System.Collections;
using System.Collections.Generic;
using Crystalbyte.Chocolate.Mvc;
using Crystalbyte.Chocolate.Scripting;

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

            ConfigureSettings(Framework.Settings);
            Framework.Initialize(app);

            if (!Framework.IsRootProcess) {
                return;
            }

            var factories = ConfigureSchemeHandlerFactories();
            factories.ForEach(Framework.SchemeFactories.Register);

            var target = CreateRenderTarget();
            var browserDelegate = CreateBrowserDelegate(target);
            Framework.Run(new HtmlRenderer(target, browserDelegate));

            Framework.Shutdown();
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e) {
            var descriptors = ConfigureSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(FrameworkSettings settings) {
#if DEBUG
            settings.LogSeverity = LogSeverity.LogseverityVerbose;
#else
            settings.LogSeverity = LogSeverity.LogseverityInfo;
#endif
        }

        protected virtual IList<SchemeHandlerFactoryDescriptor> ConfigureSchemeHandlerFactories() {
            return new List<SchemeHandlerFactoryDescriptor> {
                new SchemeHandlerFactoryDescriptor("mvc", "localhost", new MvcSchemeHandlerFactory())
            };
        }

        protected virtual IList<SchemeDescriptor> ConfigureSchemeHandlers() {
            return new List<SchemeDescriptor> {
                new MvcSchemeDescriptor()
            };
        }

        protected virtual IList<ScriptingExtension> RegisterScriptingExtensions() {
            return new List<ScriptingExtension>();
        }

        private void OnFrameworkInitialized(object sender, EventArgs e) {
            var extensions = RegisterScriptingExtensions();
            if (extensions != null) {
                extensions.ForEach(RegisterScriptingExtension);
            }
        }

        private static void RegisterScriptingExtension(ScriptingExtension extension) {
            var name = Guid.NewGuid().ToString();
            ScriptingRuntime.RegisterExtension(name, extension);
        }
    }
}