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
        private static readonly Dictionary<IRenderTarget, RenderProcess> _views;

        static Framework() {
            _settings = new FrameworkSettings();
            _views = new Dictionary<IRenderTarget, RenderProcess>();
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

            // CEF requires all objects to be freed before the window is actually closed.
            // Since this is a non recurring event which happens only when a window is closed the GC calls should not affect performance.
            // Let's not poke the GC more than required ;)
            // http://blogs.msdn.com/b/ricom/archive/2004/11/29/271829.aspx
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            CefAppCapi.CefShutdown();
            OnShutdownFinished(EventArgs.Empty);
        }

        public static void Attach(RenderProcess process) {
            _views.Add(process.RenderTarget, process);
            process.RenderTarget.TargetClosing += OnRenderTargetClosing;
            process.RenderTarget.TargetClosed += OnRenderTargetClosed;
            process.CreateBrowser();
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

        public static void Run(RenderProcess process) {
            Attach(process);
            CefAppCapi.CefRunMessageLoop();
        }

        public static void Run() {
            CefAppCapi.CefRunMessageLoop();
        }
    }
}