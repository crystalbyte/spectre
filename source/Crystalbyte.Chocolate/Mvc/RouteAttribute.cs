using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RouteAttribute : Attribute {
        public RouteAttribute(string path) {
            Path = path;
        }

        public string Path { get; set; }
    }
}
