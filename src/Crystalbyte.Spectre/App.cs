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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class App : OwnedRefCountedCefTypeAdapter {
        private readonly CefAppCapiDelegates.OnBeforeCommandLineProcessingCallback _beforeCommandLineProcessingCallback;
        private readonly CefAppCapiDelegates.GetBrowserProcessHandlerCallback _getBrowserProcessHandlerCallback;
        private readonly CefAppCapiDelegates.GetRenderProcessHandlerCallback _getRenderProcessHandlerCallback;
        private readonly CefAppCapiDelegates.OnRegisterCustomSchemesCallback _registerCustomSchemeCallback;
		private readonly CefAppCapiDelegates.GetResourceBundleHandlerCallback _getResourceBundleHandlerCallback;
		private readonly BrowserProcessHandler _browserProcessHandler;
        private readonly RenderProcessHandler _renderProcessHandler;
		private readonly ResourceBundleHandler _resourceBundleHandler;
		private readonly RendererDelegate _delegate;

        public App(RendererDelegate appDelegate)
            : base(typeof (CefApp)) {
            _delegate = appDelegate;
            _browserProcessHandler = new BrowserProcessHandler(appDelegate);
            _renderProcessHandler = new RenderProcessHandler(appDelegate);
			_resourceBundleHandler = new ResourceBundleHandler(appDelegate);

			_getResourceBundleHandlerCallback = OnGetResourceBundleHandler;
            _getRenderProcessHandlerCallback = GetRenderProcessHandler;
			_getBrowserProcessHandlerCallback = OnGetBrowserProcessHandler;

            _beforeCommandLineProcessingCallback = OnBeforeCommandLineProcessing;
            _registerCustomSchemeCallback = OnRegisterCustomScheme;

            MarshalToNative(new CefApp {
                Base = DedicatedBase,
                OnBeforeCommandLineProcessing =
                    Marshal.GetFunctionPointerForDelegate(_beforeCommandLineProcessingCallback),
                CefCallbackGetRenderProcessHandler =
                    Marshal.GetFunctionPointerForDelegate(_getRenderProcessHandlerCallback),
                OnRegisterCustomSchemes =
                    Marshal.GetFunctionPointerForDelegate(_registerCustomSchemeCallback),
                CefCallbackGetBrowserProcessHandler =
                    Marshal.GetFunctionPointerForDelegate(_getBrowserProcessHandlerCallback),
				CefCallbackGetResourceBundleHandler =
					Marshal.GetFunctionPointerForDelegate(_getResourceBundleHandlerCallback)
            });
        }

        public RendererDelegate Delegate {
            get { return _delegate; }
        }

        private void OnRegisterCustomScheme(IntPtr self, IntPtr registrar) {
            var e = new CustomSchemesRegisteringEventArgs();
            _delegate.OnCustomSchemesRegistering(e);

            using (var r = SchemeRegistrar.FromHandle(registrar)) {
                e.SchemeDescriptors.ForEach(r.Register);
            }
        }

		private IntPtr OnGetResourceBundleHandler(IntPtr self) {
			if (_resourceBundleHandler == null) {
				return IntPtr.Zero;
			}
			Reference.Increment(_resourceBundleHandler);
			return _resourceBundleHandler.Handle;
		}

        private IntPtr OnGetBrowserProcessHandler(IntPtr self) {
            if (_browserProcessHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_browserProcessHandler);
            return _browserProcessHandler.Handle;
        }

        private IntPtr GetRenderProcessHandler(IntPtr self) {
            if (_renderProcessHandler == null) {
                return IntPtr.Zero;
            }
            Reference.Increment(_renderProcessHandler);
            return _renderProcessHandler.Handle;
        }

        private void OnBeforeCommandLineProcessing(IntPtr self, IntPtr processtype, IntPtr commandline) {

            var e = new CommandLineProcessingEventArgs {
                ProcessType =
                    processtype == IntPtr.Zero
                        ? string.Empty
                        : StringUtf16.ReadString(processtype),
                CommandLine = CommandLine.FromHandle(commandline)
            };		

            _delegate.OnCommandLineProcessing(e);
        }

        protected override void DisposeManaged() {
            base.DisposeManaged();
            _resourceBundleHandler.Dispose();
            _browserProcessHandler.Dispose();
            _renderProcessHandler.Dispose();
        }
    }
}
