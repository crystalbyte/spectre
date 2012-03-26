using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public static class App {

        private static bool _isInitialized;
        private static readonly Dictionary<IRenderTarget, View> Views;

        static App() {
            Views = new Dictionary<IRenderTarget, View>();
        }

        public static void Run(AppContext context) {
            var module = context.RenderTarget.GetType().Module;
            var hInstance = Marshal.GetHINSTANCE(module);
            Run(hInstance, context);
        }

        private static void Run(IntPtr hInstance, AppContext context) {
            if (!_isInitialized) {
                var argsHandle = MarshalInstance(hInstance);
                var settingsHandle = context.Settings.NativeHandle;

                // CefApp null for now
                var result = CefAppCapi.CefInitialize(argsHandle, settingsHandle, IntPtr.Zero);
                _isInitialized = Convert.ToBoolean(result);
            }

            var target = context.RenderTarget;
            target.Closed += OnRenderTargetClosed;
            var view = new View(context);
            Views.Add(target, view);
        }

        private static void OnRenderTargetClosed(object sender, EventArgs e) {
            var target = (IRenderTarget) sender;
            target.Closed -= OnRenderTargetClosed;
            if (!Views.ContainsKey(target)) {
                return;
            }

            Views[target].Dispose();
            Views.Remove(target);

            // revive for finalization
            GC.Collect();

            // call finalizers
            GC.Collect();

            if (!Views.Any()) {
                CefAppCapi.CefShutdown();
            }
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
