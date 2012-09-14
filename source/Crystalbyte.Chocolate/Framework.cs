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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.Routing;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate {
    public static class Framework {
        private static App _app;
        private static readonly Dictionary<IRenderTarget, HtmlRenderer> _views;
        private static readonly SchemeHandlerFactoryManager _schemeHandlerfactoryManager;

        static Framework() {
            RegisterUriScheme(Schemes.Chocolate);
            RegisterUriScheme(Schemes.Pack);
            Settings = new FrameworkSettings();
            _schemeHandlerfactoryManager = new SchemeHandlerFactoryManager();
            _views = new Dictionary<IRenderTarget, HtmlRenderer>();
            QuitAfterLastViewClosed = true;
        }

        public static void RegisterUriScheme(string scheme) {
            if (!UriParser.IsKnownScheme(scheme)) {
                UriParser.Register(new GenericUriParser
                                       (GenericUriParserOptions.GenericAuthority), scheme, -1);
            }
        }

        public static FrameworkSettings Settings { get; private set; }

        public static bool IsInitialized { get; private set; }
        public static bool IsRootProcess { get; private set; }
        public static bool QuitAfterLastViewClosed { get; set; }

        public static void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public static SchemeHandlerFactoryManager SchemeFactories {
            get { return _schemeHandlerfactoryManager; }
        }

        public static event EventHandler ShutdownStarted;

        public static void OnShutdownStarted(EventArgs e) {
            var handler = ShutdownStarted;
            if (handler != null) {
                handler(null, e);
            }
        }

        public static event EventHandler ShutdownFinished;

        public static void OnShutdownFinished(EventArgs e) {
            var handler = ShutdownFinished;
            if (handler != null) {
                handler(null, e);
            }
        }

        private static bool Initialize(IntPtr mainArgs, AppDelegate del = null) {
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

        public static StreamResourceInfo GetResourceStream(Uri uri) {
            if (uri.Scheme != Schemes.Pack && uri.Scheme != Schemes.Chocolate) {
                throw new NotSupportedException("Only pack and choc uri's are supported.");
            }
            if (uri.Host.StartsWith("siteoforigin")) {
                return GetContentStreamFromLocalPath(uri);
            }
            if (uri.Host.StartsWith("application")) {
                return GetContentStreamFromAssembly(uri);
            }
            throw new NotSupportedException(string.Format("Authority '{0}' is not supported.", uri.Authority));
        }

        private static StreamResourceInfo GetContentStreamFromAssembly(Uri uri) {
            throw new NotImplementedException();
        }

        private static StreamResourceInfo GetContentStreamFromLocalPath(Uri uri) {
            var entry = Assembly.GetEntryAssembly();
            var locationInfo = new FileInfo(entry.Location);
            var directoryName = locationInfo.DirectoryName;

            if (string.IsNullOrWhiteSpace(directoryName)) {
                throw new NullReferenceException("DirectoryName is null.");
            }

            var localPath = uri.LocalPath.TrimStart('/');

            var resourcePath = Path.Combine(directoryName, localPath);
            var extension = resourcePath.ToFileExtension();
            return new StreamResourceInfo {
                ContentType = MimeMapper.ResolveFromExtension(extension),
                Stream = new MemoryStream(File.ReadAllBytes(resourcePath))
            };
        }

        public static bool Initialize(AppDelegate del = null) {
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

        public static void Shutdown() {
            OnShutdownStarted(EventArgs.Empty);

            // Force collect to remove all remaining uncollected native objects.
            // This is only called once, when closing the program, thus not affecting performance in the slightest.
            // http://blogs.msdn.com/b/ricom/archive/2004/11/29/271829.aspx
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // If CEF does not complain beyond this point we have managed to free all previously allocated native objects.
            CefAppCapi.CefShutdown();
            OnShutdownFinished(EventArgs.Empty);
        }

        public static void Add(HtmlRenderer renderer) {
            _views.Add(renderer.RenderTarget, renderer);
            renderer.RenderTarget.TargetClosing += OnRenderTargetClosing;
            renderer.RenderTarget.TargetClosed += OnRenderTargetClosed;
            renderer.CreateBrowser();
        }

        private static void OnRenderTargetClosing(object sender, EventArgs e) {
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

        public static void Run(HtmlRenderer renderer) {
            Add(renderer);
            CefAppCapi.CefRunMessageLoop();
        }

        public static void Run() {
            CefAppCapi.CefRunMessageLoop();
        }
    }
}