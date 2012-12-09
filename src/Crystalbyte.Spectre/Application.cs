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
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Threading;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class Application {
        private readonly SchemeHandlerFactoryManager _schemeHandlerfactoryManager;
        private readonly Dictionary<IRenderTarget, Viewport> _views;
        private App _app;

        static Application() {
            Current = new Application();
        }

        private Application() {
            if (Current != null) {
                throw new InvalidOperationException(
                    "Only a single framework instance may be created for each AppDomain.");
            }

            RegisterUriScheme(Schemes.Spectre);

            _views = new Dictionary<IRenderTarget, Viewport>();
            _schemeHandlerfactoryManager = new SchemeHandlerFactoryManager();

            Settings = new ApplicationSettings();
            QuitAfterLastViewClosed = true;
        }

        public static Application Current { get; private set; }

        public ApplicationSettings Settings { get; set; }
        public bool IsInitialized { get; private set; }
        public bool IsRootProcess { get; private set; }
        public bool QuitAfterLastViewClosed { get; set; }

        public IEnumerable<Viewport> Viewports {
            get { return _views.Values; }
        }

        public Dispatcher Dispatcher {
            get { return Dispatcher.Current; }
        }

        public SchemeHandlerFactoryManager SchemeFactories {
            get { return _schemeHandlerfactoryManager; }
        }

        public static void RegisterUriScheme(string scheme) {
            if (!UriParser.IsKnownScheme(scheme)) {
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), scheme, -1);
            }
        }

        public void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public event EventHandler ShutdownStarted;

        public void OnShutdownStarted(EventArgs e) {
            var handler = ShutdownStarted;
            if (handler != null) {
                handler(this, e);
            }
        }

        public event EventHandler ShutdownFinished;

        public void OnShutdownFinished(EventArgs e) {
            var handler = ShutdownFinished;
            if (handler != null) {
                handler(this, e);
            }
        }

        public RenderDelegate Delegate {
            get { return _app.Delegate; }
        }

        private bool Initialize(IntPtr mainArgs, RenderDelegate del = null) {
            _app = new App(del ?? new RenderDelegate());

            Reference.Increment(_app.Handle);
            var exitCode = CefAppCapi.CefExecuteProcess(mainArgs, _app.Handle);
            IsRootProcess = exitCode < 0;
            if (!IsRootProcess) {
                return true;
            }

            Reference.Increment(_app.Handle);
            var result = CefAppCapi.CefInitialize(mainArgs, Settings.Handle, _app.Handle);
            IsInitialized = Convert.ToBoolean(result);
            return IsInitialized;
        }

        public bool Initialize(RenderDelegate del = null) {
            var handle = IntPtr.Zero;

            if (Platform.IsLinux) {
                var commandLine = Environment.GetCommandLineArgs();
                handle = AppArguments.CreateForLinux(commandLine);
            }

            if (Platform.IsWindows) {
                var module = Assembly.GetEntryAssembly().ManifestModule;
                var hInstance = Marshal.GetHINSTANCE(module);
                handle = AppArguments.CreateForWindows(hInstance);
            }
            return Initialize(handle, del);
        }

        public void Shutdown() {
            OnShutdownStarted(EventArgs.Empty);

            // Poke the GC to free uncollected native objects.
            // This is only called when closing the program, thus not affecting performance.
            // http://blogs.msdn.com/b/ricom/archive/2004/11/29/271829.aspx
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // If CEF does not complain beyond this point we have managed to free all previously allocated native objects.
            CefAppCapi.CefShutdown();
            OnShutdownFinished(EventArgs.Empty);
        }

        public void Add(Viewport renderer) {
            _views.Add(renderer.RenderTarget, renderer);
            renderer.RenderTarget.TargetClosing += OnRenderTargetClosing;
            renderer.RenderTarget.TargetClosed += OnRenderTargetClosed;
            renderer.CreateBrowser();
        }

        private void OnRenderTargetClosing(object sender, EventArgs e) {
            var target = (IRenderTarget) sender;
            target.TargetClosing -= OnRenderTargetClosing;
            target.Dispose();
            _views[target].Dispose();
            _views.Remove(target);

            if (QuitAfterLastViewClosed && _views.Count < 1) {
                CefAppCapi.CefQuitMessageLoop();
            }
        }

        private static void OnRenderTargetClosed(object sender, EventArgs e) {
            var target = (IRenderTarget) sender;
            target.TargetClosed -= OnRenderTargetClosed;
        }

        public event EventHandler Starting;

        public void OnStarting(EventArgs e) {
            var handler = Starting;
            if (handler != null) {
                handler(this, e);
            }
        }

        public void Run(Viewport viewport) {
            Add(viewport);
            Run();
        }

        public void Run() {
            OnStarting(EventArgs.Empty);
            CefAppCapi.CefRunMessageLoop();
        }
    }
}
