#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Bindings.Internal;
using System.Threading;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public static class Framework {
        private static readonly App _app;
        private static readonly FrameworkSettings _settings;
        private static readonly Dictionary<IRenderTarget, RenderProcess> _views;

        static Framework() {
            _app = new App();
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

        public static void Attach(RenderProcess process) {
            _views.Add(process.RenderTarget, process);
            process.RenderTarget.TargetClosing += OnRenderTargetClosing;
            process.RenderTarget.TargetClosed += OnRenderTargetClosed;
            process.CreateBrowser();
        }

        private static void OnRenderTargetClosing(object sender, EventArgs e) {
            var target = (IRenderTarget)sender;
            target.TargetClosing -= OnRenderTargetClosing;
            _views[target].Dispose();
            _views.Remove(target);
            if (QuitAfterLastViewClosed && _views.Count < 1) {
                 CefAppCapi.CefQuitMessageLoop();
            }
        }

        private static void OnRenderTargetClosed(object sender, EventArgs e) {
            var target = (IRenderTarget)sender;
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