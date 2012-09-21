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
using System.Linq;
using System.Reflection;

#endregion

namespace Crystalbyte.Chocolate.Web {
    public sealed class ChocSchemeDescriptor : SchemeDescriptor {
        public ChocSchemeDescriptor() {
            _assemblyCatalog = new List<Assembly>();

            SchemeProperties = SchemeProperties.Local 
                | SchemeProperties.DisplayIsolated 
                | SchemeProperties.Standard;
        }

        public override string Scheme {
            get { return Schemes.Choc; }
        }

        private readonly List<Assembly> _assemblyCatalog;

        public IList<Assembly> AssemblyCatalog {
            get { return _assemblyCatalog; }
        }

        public override void OnRegistered(EventArgs e) {
            base.OnRegistered(e);

            // Add entry assembly by default
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null && !AssemblyCatalog.Contains(entryAssembly)) {
                AssemblyCatalog.Add(entryAssembly);
            }

            //var controllers = QueryControllers();
            //controllers.ForEach(x => {
            //    var routes = x.GetCustomAttributes(typeof (RouteAttribute), true)
            //        .Cast<RouteAttribute>();
            //    routes.ForEach(r => RouteRegistrar.Register(r.Path, x));
            //});
        }

        private IEnumerable<Type> QueryControllers() {
            return AssemblyCatalog.SelectMany(x => x.GetTypes()
                .Where(type => typeof (IViewController).IsAssignableFrom(type)));
        }
    }
}