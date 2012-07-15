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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.Composition {
    public abstract class MefBootstrapper : Bootstrapper {
        [ImportMany(typeof (SchemeHandlerFactory))]
        public IEnumerable<Lazy<SchemeHandlerFactory, ISchemeMetadata>> SchemeHandlers { get; set; }

        [ImportMany(typeof (ScriptingExtension))]
        public IEnumerable<Lazy<ScriptingExtension, IScriptingExtensionMetadata>> ScriptingExtensions { get; set; }

        protected override void ConfigureScriptingRuntime() {
            ScriptingExtensions.ForEach(x => ScriptingRuntime.RegisterExtension(x.Metadata.Name, x.Value));
        }

        protected override void InitializeSchemeHandlers() {
            SchemeHandlers.ForEach(x => Scheme.Register(x.Metadata.SchemeName, x.Metadata.Domain ?? string.Empty, x.Value));
        }

        public override void Run() {
            var catalog = new AggregateCatalog();
            InitializeCatalog(catalog);
            var composition = new CompositionContainer(catalog);
            composition.ComposeParts(this);
            base.Run();
        }

        public virtual void InitializeCatalog(AggregateCatalog catalog) {}
    }
}