using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public static class Application {
        private static bool _isInitialized;
        private static readonly AppContext _app; 
        private static readonly Dictionary<IRenderTarget, View> _views;
        private static readonly ApplicationSettings _settings;

        static Application() {
            _app = new AppContext();
            _settings = new ApplicationSettings();
            _views = new Dictionary<IRenderTarget, View>();
        }

        public static ApplicationSettings Settings { get { return _settings; } }

        public static void IterateMessageLoop() {
            CefAppCapi.CefDoMessageLoopWork();
        }

        public static void Register(View view) {
            var module = view.RenderTarget.GetType().Module;
            var hInstance = Marshal.GetHINSTANCE(module);
            Register(hInstance, view);
        }

        private static void Initialize(IntPtr hInstance) {
            var appHandle = _app.NativeHandle;
            var argsHandle = MarshalInstance(hInstance);
            var settingsHandle = _settings.NativeHandle;
            // CefApp null for now
            var result = CefAppCapi.CefInitialize(argsHandle, settingsHandle, appHandle);
            _isInitialized = Convert.ToBoolean(result);
        }

        public static void Shutdown() {
            CefAppCapi.CefShutdown();
            _isInitialized = false;
        }

        private static void Register(IntPtr hInstance, View view) {
            if (!_isInitialized) {
                Initialize(hInstance);
            }
            _views.Add(view.RenderTarget, view);
            view.RenderTarget.TargetClosing += OnRenderTargetClosed;
            view.CreateBrowser();
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

        private static IntPtr MarshalInstance(IntPtr hInstance) {
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
