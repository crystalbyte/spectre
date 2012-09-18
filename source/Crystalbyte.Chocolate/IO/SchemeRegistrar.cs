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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;

#endregion

namespace Crystalbyte.Chocolate.IO {
    public sealed class SchemeRegistrar : RefCountedNativeObject {
        private SchemeRegistrar(IntPtr handle)
            : base(typeof (CefSchemeRegistrar)) {
            NativeHandle = handle;
        }

        public static SchemeRegistrar FromHandle(IntPtr handle) {
            return new SchemeRegistrar(handle);
        }

        public void Register(SchemeDescriptor descriptor) {
            var r = MarshalFromNative<CefSchemeRegistrar>();
            var function = (AddCustomSchemeCallback)
                           Marshal.GetDelegateForFunctionPointer(r.AddCustomScheme, typeof (AddCustomSchemeCallback));

            var name = new StringUtf16(descriptor.Scheme);

            var isStandard = descriptor.SchemeProperties.HasFlag(SchemeProperties.Standard) ? 1 : 0;
            var isLocal = descriptor.SchemeProperties.HasFlag(SchemeProperties.Local) ? 1 : 0;
            var isDisplayIsolated = descriptor.SchemeProperties.HasFlag(SchemeProperties.DisplayIsolated) ? 1 : 0;

            var result = function(NativeHandle, name.NativeHandle, isStandard, isLocal, isDisplayIsolated);
            var success = Convert.ToBoolean(result);
            if (!success) {
                Debug.WriteLine("Error registering custom scheme '{0}'", descriptor.Scheme);
            }
            else {
                descriptor.OnRegistered(EventArgs.Empty);
            }
        }
    }
}