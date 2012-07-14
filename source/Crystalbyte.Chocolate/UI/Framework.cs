#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

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
            var result = CefAppCapi.CefInitialize(mainArgs, _settings.NativeHandle, _app.NativeHandle);
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