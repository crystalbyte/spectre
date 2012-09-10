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

#endregion

namespace Crystalbyte.Chocolate {
    public abstract class SchemeHandlerFactory : RefCountedNativeObject {
        private readonly CreateCallback _createCallback;

        protected SchemeHandlerFactory()
            : base(typeof (CefSchemeHandlerFactory)) {
            _createCallback = Create;
            MarshalToNative(new CefSchemeHandlerFactory {
                Base = DedicatedBase,
                Create = Marshal.GetFunctionPointerForDelegate(_createCallback)
            });
        }

        private IntPtr Create(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemename, IntPtr request) {
            var e = new CreateHandlerEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Scheme = StringUtf16.ReadString(schemename)
            };
            return OnCreateHandler(this, e).NativeHandle;
        }

        protected abstract ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e);
    }
}