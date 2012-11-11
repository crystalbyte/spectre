#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class App : OwnedRefCountedNativeObject {
        private readonly CefAppCapiDelegates.OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;
        private readonly BrowserProcessHandler _browserProcessHandler;
        private readonly AppDelegate _delegate;
        private readonly CefAppCapiDelegates.GetBrowserProcessHandlerCallback _getBrowserProcessHandlerCallback;
        private readonly CefAppCapiDelegates.GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly CefAppCapiDelegates.OnRegisterCustomSchemesCallback _registerCustomSchemeCallback;
        private readonly RenderProcessHandler _renderProcessHandler;

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

        public AppDelegate Delegate {
            get { return _delegate; }
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
                ProcessType =
                    processtype == IntPtr.Zero
                        ? string.Empty
                        : StringUtf16.ReadString(processtype),
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
