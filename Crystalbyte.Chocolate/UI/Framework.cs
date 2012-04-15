#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public static class Framework {
        private static App _app;
        private static readonly FrameworkSettings _settings;
        private static readonly Dictionary<IRenderTarget, HtmlRenderer> _views;

        static Framework() {
            _settings = new FrameworkSettings();
            _views = new Dictionary<IRenderTarget, HtmlRenderer>();
            QuitAfterLastViewClosed = true;
        }

        public static FrameworkSettings Settings {
            get { return _settings; }
        }

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

        public static bool Initialize(Module module, AppDelegate appDelegate = null) {
            var hInstance = Marshal.GetHINSTANCE(module);
            var argsHandle = MarshalMainArgs(hInstance);

            _app = new App(appDelegate ?? new AppDelegate());
            var appHandle = _app.NativeHandle;

            Reference.Increment(appHandle);
            var exitCode = CefAppCapi.CefExecuteProcess(argsHandle, appHandle);
            IsRootProcess = exitCode < 0;
            if (!IsRootProcess) {
                return true;
            }

            var settingsHandle = _settings.NativeHandle;
            Reference.Increment(appHandle);
            var result = CefAppCapi.CefInitialize(argsHandle, settingsHandle, appHandle);
            IsInitialized = Convert.ToBoolean(result);
            return IsInitialized;
        }

        public static void Shutdown() {
            OnShutdownStarted(EventArgs.Empty);

            // Force collect to remove Dipose all remaining floating native objects.
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

        private static IntPtr MarshalMainArgs(IntPtr hInstance) {
            var mainArgs = new CefMainArgs {
                Instance = hInstance
            };
            var size = Marshal.SizeOf(typeof (CefMainArgs));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
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