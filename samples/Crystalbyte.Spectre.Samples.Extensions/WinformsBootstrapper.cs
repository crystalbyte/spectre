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

using Crystalbyte.Spectre.Samples.Commands;
using Crystalbyte.Spectre.Samples.Extensions;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using System;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public sealed class WinformsBootstrapper : Bootstrapper {

        protected override void ConfigureSettings(ApplicationSettings settings)
        {
            settings.IsSingleProcess = true;
            base.ConfigureSettings(settings);
        }
       
        protected override IList<RuntimeCommand> RegisterScriptingExtensions() {
            var extensions = base.RegisterScriptingExtensions();
            extensions.Add(new SingleResultExtension());
            extensions.Add(new AsyncResultExtension());
            return extensions;
        }

        protected override IRenderTarget CreateRenderTarget() {
            return new Window {
                StartupUri = new Uri("spectre://localhost/Views/index.html")
            };
        }
    }
}