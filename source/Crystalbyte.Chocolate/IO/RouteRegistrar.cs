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
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Chocolate.IO {
    internal sealed class RouteRegistrar {
        private readonly Dictionary<string, Type> _types;

        public RouteRegistrar() {
            if (Current != null) {
                throw new NotSupportedException("Only a single instance may be created for each AppDomain.");
            }
            _types = new Dictionary<string, Type>();

            RouteRegistrar.Current = this;
        }

        public static RouteRegistrar Current { get; private set; }

        public void Register(string url, Type controller) {
            _types.Add(url, controller);
        }

        public Type GetController(string route) {
            return _types[route];
        }

        public bool TryGetController(string url, out Type controller)
        {
            if (_types.ContainsKey(url))
            {
                controller = _types[url];
                return true;
            }

            controller = null;
            return false;
        }

        public bool IsKnownRoute(string url)
        {
            return _types.ContainsKey(url);
        }
    }
}