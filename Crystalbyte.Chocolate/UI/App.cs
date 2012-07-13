#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class App : OwnedAdapter {
        private readonly OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;
        private readonly AppDelegate _delegate;
        private readonly GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly RenderProcessHandler _renderProcessHandler;

        public App(AppDelegate @delegate)
            : base(typeof (CefApp)) {
            _delegate = @delegate;
            _renderProcessHandler = new RenderProcessHandler(@delegate);
            _getRenderProcessHandlerCallback = GetRenderProcessHandler;
            _beforeCommandLineProcessingCallback = OnCommandLineProcessing;

            MarshalToNative(new CefApp {
                Base = DedicatedBase,
                OnBeforeCommandLineProcessing =
                                Marshal.GetFunctionPointerForDelegate(_beforeCommandLineProcessingCallback),
                CefCallbackGetRenderProcessHandler =
                                Marshal.GetFunctionPointerForDelegate(_getRenderProcessHandlerCallback),
            });
        }

        private IntPtr GetRenderProcessHandler(IntPtr self) {
            if (_renderProcessHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_renderProcessHandler.NativeHandle);
            return _renderProcessHandler.NativeHandle;
        }

        private void OnCommandLineProcessing(IntPtr self, IntPtr processtype, IntPtr commandline) {
            var e = new ProcessStartedEventArgs {
                ProcessType = StringUtf16.ReadString(processtype),
                CommandLine = CommandLine.FromHandle(commandline)
            };
            _delegate.OnProcessStarted(e);
        }

        protected override void DisposeNative() {
            _renderProcessHandler.Dispose();
            base.DisposeNative();
        }
    }
}