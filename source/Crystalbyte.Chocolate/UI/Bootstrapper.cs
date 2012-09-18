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
using Crystalbyte.Chocolate.IO;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public abstract class Bootstrapper {
        private readonly Framework _framework;
        protected abstract IRenderTarget CreateRenderTarget();

        protected Bootstrapper() {
            _framework = new Framework();
        }

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

            ConfigureSettings(_framework.Settings);
            _framework.Initialize(app);

            if (!_framework.IsRootProcess) {
                return;
            }

            var factories = RegisterSchemeHandlerFactories();
            factories.ForEach(_framework.SchemeFactories.Register);

            var target = CreateRenderTarget();
            var browserDelegate = CreateBrowserDelegate(target);

            _framework.Run(new Viewport(target, browserDelegate));
            _framework.Shutdown();
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e) {
            var descriptors = RegisterSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(FrameworkSettings settings) {
#if DEBUG
            settings.LogSeverity = LogSeverity.LogseverityVerbose;
#else
            settings.LogSeverity = LogSeverity.LogseverityInfo;
#endif
        }

        protected virtual IList<SchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories() {
            return new List<SchemeHandlerFactoryDescriptor> {
                new SchemeHandlerFactoryDescriptor(Schemes.Pack, string.Empty, new PackSchemeHandlerFactory())
            };
        }

        protected virtual IList<SchemeDescriptor> RegisterSchemeHandlers() {
            return new List<SchemeDescriptor> {
                new PackSchemeDescriptor()
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