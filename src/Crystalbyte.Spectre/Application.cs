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
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
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

        private bool Initialize(IntPtr mainArgs, AppDelegate del = null) {
            _app = new App(del ?? new AppDelegate());

            Reference.Increment(_app.NativeHandle);
            var exitCode = CefAppCapi.CefExecuteProcess(mainArgs, _app.NativeHandle);
            IsRootProcess = exitCode < 0;
            if (!IsRootProcess) {
                return true;
            }

            Reference.Increment(_app.NativeHandle);
            var result = CefAppCapi.CefInitialize(mainArgs, Settings.NativeHandle, _app.NativeHandle);
            IsInitialized = Convert.ToBoolean(result);
            return IsInitialized;
        }

        public bool Initialize(AppDelegate del = null) {
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
