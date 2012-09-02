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
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    internal sealed class App : RefCountedNativeObject {
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