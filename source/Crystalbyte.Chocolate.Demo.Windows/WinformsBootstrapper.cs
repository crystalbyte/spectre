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
using System.Web.Mvc;
using System.Web.Routing;
using Crystalbyte.Chocolate.UI;
using Crystalbyte.Chocolate.Web;
using Crystalbyte.Chocolate.Web.Mvc;

#endregion

namespace Crystalbyte.Chocolate {
    public sealed class WinformsBootstrapper : Bootstrapper {

        public WinformsBootstrapper() {
            Application.Current.Starting += OnApplicationStarting;
        }

        private static void OnApplicationStarting(object sender, EventArgs e) {
              RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new {
                                controller = "Desktop",
                                action = "Index",
                                id = UrlParameter.Optional
                            });
        }

        protected override IRenderTarget CreateRenderTarget() {
            return new Window {
                StartupUri = new Uri("chocolate://localhost/Views/Index.html")
                //StartupUri = new Uri("chocolate://localhost/views/desktop")
            };
        }

        protected override IList<ISchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories() {
            var handlers = base.RegisterSchemeHandlerFactories();

            var descriptor = handlers.OfType<ChocolateSchemeHandlerFactoryDescriptor>().First();
            descriptor.Register(typeof (MvcRequestModule));

            return handlers;
        }
    }
}