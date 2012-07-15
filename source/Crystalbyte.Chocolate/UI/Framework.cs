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
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public static class Framework {
        private static App _app;
        private static readonly Dictionary<IRenderTarget, HtmlRenderer> Views;

        static Framework() {
            Settings = new FrameworkSettings();
            Views = new Dictionary<IRenderTarget, HtmlRenderer>();
            QuitAfterLastViewClosed = true;
        }

        public static FrameworkSettings Settings { get; private set; }

        public static bool IsInitialized { get; private set; }
        public static bool IsRootProcess { get; private set; }
        public static bool QuitAfterLastViewClosed { get; set; }

        public static void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
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

        public static bool Initialize(string[] argv, AppDelegate del = null) {
            if (!Platform.IsOSX) {
                throw new InvalidOperationException("Platform must be OS X for this overload.");
            }
            var mainArgs = AppArguments.CreateForMac(argv);
            return InitializeInternal(mainArgs, del);
        }

        private static bool InitializeInternal(IntPtr mainArgs, AppDelegate del = null) {
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


        public static bool Initialize(Module module, AppDelegate del = null) {
            if (!Platform.IsWindows) {
                throw new InvalidOperationException("Platform must be Windows for this overload.");
            }
            var hInstance = Marshal.GetHINSTANCE(module);
            var mainArgs = AppArguments.CreateForWindows(hInstance);
            return InitializeInternal(mainArgs, del);
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
            Views.Add(renderer.RenderTarget, renderer);
            renderer.RenderTarget.TargetClosing += OnRenderTargetClosing;
            renderer.RenderTarget.TargetClosed += OnRenderTargetClosed;
            renderer.CreateBrowser();
        }

        private static void OnRenderTargetClosing(object sender, EventArgs e) {
            var target = (IRenderTarget) sender;
            target.TargetClosing -= OnRenderTargetClosing;
            Views[target].Dispose();
            Views.Remove(target);
            if (QuitAfterLastViewClosed && Views.Count < 1) {
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