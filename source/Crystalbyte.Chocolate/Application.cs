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
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.UI;
using Crystalbyte.Chocolate.Web;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class Application {
        private App _app;
        private readonly Dictionary<IRenderTarget, Viewport> _views;
        private readonly SchemeHandlerFactoryManager _schemeHandlerfactoryManager;

        static Application() {
            Current = new Application();
        }

        private Application() {
            if (Current != null) {
                throw new InvalidOperationException(
                    "Only a single framework instance may be created for each AppDomain.");
            }

            RegisterUriScheme(Schemes.Chocolate);

            _views = new Dictionary<IRenderTarget, Viewport>();
            _schemeHandlerfactoryManager = new SchemeHandlerFactoryManager();

            Settings = new ApplicationSettings();
            QuitAfterLastViewClosed = true;
        }

        public static void RegisterUriScheme(string scheme) {
            if (!UriParser.IsKnownScheme(scheme)) {
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), scheme, -1);
            }
        }

        public static Application Current { get; private set; }

        public ApplicationSettings Settings { get; set; }
        public bool IsInitialized { get; private set; }
        public bool IsRootProcess { get; private set; }
        public bool QuitAfterLastViewClosed { get; set; }

        public void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public SchemeHandlerFactoryManager SchemeFactories {
            get { return _schemeHandlerfactoryManager; }
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

        public void Run(Viewport renderer) {
            Add(renderer);
            Run();
        }

        public void Run() {
            OnStarting(EventArgs.Empty);
            CefAppCapi.CefRunMessageLoop();
        }
    }
}