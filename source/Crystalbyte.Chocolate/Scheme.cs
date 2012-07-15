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
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate {
    public static class Scheme {
        public static void Register(string scheme, string domain, SchemeHandlerFactory factory) {
            if (scheme == null) {
                throw new ArgumentNullException("scheme");
            }
            if (domain == null) {
                throw new ArgumentNullException("domain");
            }
            if (factory == null) {
                throw new ArgumentNullException("factory");
            }

            var s = new StringUtf16(scheme);
            var d = new StringUtf16(domain);

            CefSchemeCapi.CefRegisterSchemeHandlerFactory(s.NativeHandle, d.NativeHandle, factory.NativeHandle);

            d.Free();
            s.Free();
        }

        public static void Clear() {
            CefSchemeCapi.CefClearSchemeHandlerFactories();
        }
    }
}