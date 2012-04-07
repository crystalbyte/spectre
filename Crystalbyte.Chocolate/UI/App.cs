#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class App : OwnedAdapter {
        private readonly AppDelegate _delegate;
        private readonly RenderProcessHandler _renderProcessHandler;
        private readonly GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;

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
            var e = new CommandLineEventArgs {
                ProcessType = StringUtf16.ReadString(processtype),
                CommandLine = CommandLine.FromHandle(commandline)
            };
            _delegate.OnCommandLineProcessing(e);
        }
    }
}