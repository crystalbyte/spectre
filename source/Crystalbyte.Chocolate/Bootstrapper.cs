#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System.IO;

#endregion

#region Namespace directives

using System;
using System.Reflection;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public abstract class Bootstrapper {
        protected abstract IRenderTarget CreateRenderTarget();

        protected virtual AppDelegate CreateAppDelegate() {
            return new AppDelegate();
        }

        protected virtual BrowserDelegate CreateBrowserDelegate(IRenderTarget target) {
            return new BrowserDelegate();
        }

        public virtual void Run() {
            var @delegate = CreateAppDelegate();
            @delegate.Initialized += OnFrameworkInitialized;

			InitializeFramework();
            Framework.Initialize(@delegate);

            if (!Framework.IsRootProcess) {
                return;
            }

			InitializeRenderer();
            InitializeSchemeHandlers();

            var target = CreateRenderTarget();
            var browserDelegate = CreateBrowserDelegate(target);
            Framework.Run(new HtmlRenderer(target, browserDelegate));

            Framework.Shutdown();
        }

	
		protected virtual void ConfigureScriptingRuntime() { }
		protected virtual void InitializeFramework() { }
		protected virtual void InitializeRenderer() { }
        protected virtual void InitializeSchemeHandlers() { }

        private void OnFrameworkInitialized(object sender, EventArgs e) {
            ConfigureScriptingRuntime();
        }
    }
}