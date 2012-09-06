using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    public sealed class RazorView : IView
    {
        public string Compose() {
            return "<html><head></head><body style=\"background-color: green;\">Razor View</body></html>";
        }
    }
}
