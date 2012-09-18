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

#endregion

#region Namespace directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Crystalbyte.Chocolate.IO;
using Crystalbyte.Chocolate.UI;

#endregion

namespace Crystalbyte.Chocolate.Razor {
    /// <summary>
    ///   The RazorView class will create the html source from the razor template and a given model.
    ///   See: http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
    ///   See: http://msdn.microsoft.com/en-us/vs2010trainingcourse_aspnetmvc3razor.aspx
    /// </summary>
    public sealed class RazorView : IView {
        private readonly Uri _templateUri;
        private readonly object _context;
        private readonly RazorStringHostContainer _host;
        private bool _isStarted;

        public RazorView(string templatePath, object context) {
            ReferencedAssemblies = new List<Assembly>();
            _templateUri = new Uri(templatePath);
            _context = context;
            _host = new RazorStringHostContainer {
                UseAppDomain = false
            };
        }

        public IList<Assembly> ReferencedAssemblies { get; private set; }

        public string Compose() {
            if (!_isStarted) {
                var location = _context.GetType().Assembly.Location;
                var name = new FileInfo(location).Name;
                _host.ReferencedAssemblies.Add(name);
                _host.Start();
                _isStarted = true;
            }
            
            try {
                var info = Framework.GetResourceStream(_templateUri);
                if (info == null) {
                    throw new NullReferenceException(string.Format("Resource '{0}' could not be found.", _templateUri));
                }

                var template = info.Stream.ToUtf8String();
                //var markup = _host.RenderTemplate(template, _context);

                //return string.IsNullOrEmpty(_host.ErrorMessage) ?
                //    new CompositionResult(markup) :
                //    new CompositionResult(string.Empty, new[] { new Exception(_host.ErrorMessage) });
                return string.Empty;
            }
            catch (Exception ex) {
                //return new CompositionResult(string.Empty, new[] {ex});
                return string.Empty;
            }
        }
    }
}