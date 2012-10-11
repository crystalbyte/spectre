#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre{
    internal sealed class App : OwnedRefCountedNativeObject{
        private readonly OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;
        private readonly BrowserProcessHandler _browserProcessHandler;
        private readonly AppDelegate _delegate;
        private readonly GetBrowserProcessHandlerCallback _getBrowserProcessHandlerCallback;
        private readonly GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly OnRegisterCustomSchemesCallback _registerCustomSchemeCallback;
        private readonly RenderProcessHandler _renderProcessHandler;

        public App(AppDelegate appDelegate)
            : base(typeof (CefApp)){
            _delegate = appDelegate;
            _browserProcessHandler = new BrowserProcessHandler(appDelegate);
            _renderProcessHandler = new RenderProcessHandler(appDelegate);
            _getRenderProcessHandlerCallback = GetRenderProcessHandler;
            _beforeCommandLineProcessingCallback = OnCommandLineProcessing;
            _registerCustomSchemeCallback = OnRegisterCustomScheme;
            _getBrowserProcessHandlerCallback = OnGetBrowserProcessHandler;

            MarshalToNative(new CefApp{
                                          Base = DedicatedBase,
                                          OnBeforeCommandLineProcessing =
                                              Marshal.GetFunctionPointerForDelegate(_beforeCommandLineProcessingCallback),
                                          CefCallbackGetRenderProcessHandler =
                                              Marshal.GetFunctionPointerForDelegate(_getRenderProcessHandlerCallback),
                                          OnRegisterCustomSchemes =
                                              Marshal.GetFunctionPointerForDelegate(_registerCustomSchemeCallback),
                                          CefCallbackGetBrowserProcessHandler =
                                              Marshal.GetFunctionPointerForDelegate(_getBrowserProcessHandlerCallback)
                                      });
        }

        private void OnRegisterCustomScheme(IntPtr self, IntPtr registrar){
            var e = new CustomSchemesRegisteringEventArgs();
            _delegate.OnCustomSchemesRegistering(e);

            using (var r = SchemeRegistrar.FromHandle(registrar)){
                e.SchemeDescriptors.ForEach(r.Register);
            }
        }

        private IntPtr OnGetBrowserProcessHandler(IntPtr self){
            if (_browserProcessHandler == null){
                return IntPtr.Zero;
            }
            Reference.Increment(_browserProcessHandler.NativeHandle);
            return _browserProcessHandler.NativeHandle;
        }

        private IntPtr GetRenderProcessHandler(IntPtr self){
            if (_renderProcessHandler == null){
                return IntPtr.Zero;
            }
            Reference.Increment(_renderProcessHandler.NativeHandle);
            return _renderProcessHandler.NativeHandle;
        }

        private void OnCommandLineProcessing(IntPtr self, IntPtr processtype, IntPtr commandline){
            var e = new ProcessStartedEventArgs{
                                                   ProcessType =
                                                       processtype == IntPtr.Zero
                                                           ? string.Empty
                                                           : StringUtf16.ReadString(processtype),
                                                   CommandLine = CommandLine.FromHandle(commandline)
                                               };
            _delegate.OnProcessStarted(e);
        }

        protected override void DisposeNative(){
            _renderProcessHandler.Dispose();
            base.DisposeNative();
        }
    }
}