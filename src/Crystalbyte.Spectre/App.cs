#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.UI;
using Crystalbyte.Chocolate.Web;

#endregion

namespace Crystalbyte.Chocolate {
    internal sealed class App : RetainedNativeObject {
        private readonly AppDelegate _delegate;

        private readonly RenderProcessHandler _renderProcessHandler;
        private readonly BrowserProcessHandler _browserProcessHandler;
        private readonly OnRegisterCustomSchemesCallback _registerCustomSchemeCallback;
        private readonly GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;
        private readonly GetBrowserProcessHandlerCallback _getBrowserProcessHandlerCallback;

        public App(AppDelegate appDelegate)
            : base(typeof (CefApp)) {
            _delegate = appDelegate;
            _browserProcessHandler = new BrowserProcessHandler(appDelegate);
            _renderProcessHandler = new RenderProcessHandler(appDelegate);
            _getRenderProcessHandlerCallback = GetRenderProcessHandler;
            _beforeCommandLineProcessingCallback = OnCommandLineProcessing;
            _registerCustomSchemeCallback = OnRegisterCustomScheme;
            _getBrowserProcessHandlerCallback = OnGetBrowserProcessHandler;

            MarshalToNative(new CefApp {
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

        private void OnRegisterCustomScheme(IntPtr self, IntPtr registrar) {
            var e = new CustomSchemesRegisteringEventArgs();
            _delegate.OnCustomSchemesRegistering(e);

            using (var r = SchemeRegistrar.FromHandle(registrar)) {
                e.SchemeDescriptors.ForEach(r.Register);
            }
        }

        private IntPtr OnGetBrowserProcessHandler(IntPtr self) {
            if (_browserProcessHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_browserProcessHandler.NativeHandle);
            return _browserProcessHandler.NativeHandle;
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
                ProcessType = processtype == IntPtr.Zero ? string.Empty : StringUtf16.ReadString(processtype),
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