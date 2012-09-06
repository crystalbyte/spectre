using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    /// <summary>
    /// The ViewResult class will create the html source from the razor source template and a given module.
    /// See: http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
    /// </summary>
    public interface IView {
        string Compose();
    }
}
