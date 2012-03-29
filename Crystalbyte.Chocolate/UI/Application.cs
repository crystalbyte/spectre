using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public static class Application {
        private static bool _isInitialized;
        private static readonly App _app; 
        private static readonly Dictionary<IRenderTarget, View> _views;
        private static readonly ApplicationSettings _settings;

        static Application() {
            _app = new App();
            _settings = new ApplicationSettings();
            _views = new Dictionary<IRenderTarget, View>();
        }

        public static ApplicationSettings Settings {
            get { return _settings; }
        }

        public static void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public static void RunMessageLoop() {
            CefAppCapi.CefRunMessageLoop();
        }

        public static void Initialize(Module module) {
            var hInstance = Marshal.GetHINSTANCE(module);
            var appHandle = _app.NativeHandle;
            var argsHandle = MarshalMainArgs(hInstance);

            var exitCode = CefAppCapi.CefExecuteProcess(argsHandle, IntPtr.Zero);
            if (exitCode >= 0) {
                return;
            }

            var settingsHandle = _settings.NativeHandle;
            var result = CefAppCapi.CefInitialize(argsHandle, settingsHandle, appHandle);
            _isInitialized = Convert.ToBoolean(result);
        }

        public static void Shutdown() {
            CefAppCapi.CefShutdown();
            _isInitialized = false;
        }

        public static void Register(View view) {
            
            _views.Add(view.RenderTarget, view);
            view.RenderTarget.TargetClosing += OnRenderTargetClosed;
            view.CreateBrowser();
        }

        public static void LaunchProcess(Module host) {
            if (!_isInitialized) {
                Initialize(host);
            }
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
        }

        private static IntPtr MarshalMainArgs(IntPtr hInstance) {
            var mainArgs = new CefMainArgs {
                Instance = hInstance
            };
            var size = Marshal.SizeOf(typeof(CefMainArgs));
            var handle = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(mainArgs, handle, false);
            return handle;
        }
    }
}
