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
        private static readonly App _app;
        private static readonly FrameworkSettings _settings;
        private static readonly Dictionary<IRenderTarget, Renderer> _views;

        static Framework() {
            _app = new App();
            _settings = new FrameworkSettings();
            _views = new Dictionary<IRenderTarget, Renderer>();
        }

        public static FrameworkSettings Settings {
            get { return _settings; }
        }

        public static bool IsInitialized { get; private set; }
        public static bool IsRootProcess { get; private set; }

        public static void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public static bool Initialize(Module module) {
            var hInstance = Marshal.GetHINSTANCE(module);
            var argsHandle = MarshalMainArgs(hInstance);

            var exitCode = CefAppCapi.CefExecuteProcess(argsHandle, IntPtr.Zero);
            IsRootProcess = exitCode < 0;

            var settingsHandle = _settings.NativeHandle;
            var appHandle = _app.NativeHandle;

            var result = CefAppCapi.CefInitialize(argsHandle, settingsHandle, appHandle);
            IsInitialized = Convert.ToBoolean(result);
            return IsInitialized;
        }

        public static void Shutdown() {
            CefAppCapi.CefShutdown();
        }

        public static void AttachRenderer(Renderer renderer) {
            _views.Add(renderer.RenderTarget, renderer);
            renderer.RenderTarget.TargetClosing += OnRenderTargetClosed;
            renderer.CreateBrowser();
        }

        private static void OnRenderTargetClosed(object sender, EventArgs e) {
            var target = (IRenderTarget) sender;
            target.TargetClosing -= OnRenderTargetClosed;
            if (!_views.ContainsKey(target)) {
                return;
            }
            _views[target].Dispose();
            _views.Remove(target);
            // revive for finalization
            GC.Collect();
            // call finalizers
            GC.Collect();

            if (_views.Count < 1) {
                CefAppCapi.CefQuitMessageLoop();
            }
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

        public static void Run(Renderer renderer) {
            AttachRenderer(renderer);
            CefAppCapi.CefRunMessageLoop();
        }

        public static void Run() {
            CefAppCapi.CefRunMessageLoop();
        }
    }
}