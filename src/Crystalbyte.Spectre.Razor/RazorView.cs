using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public class RazorView : IView {
        private readonly object _model;

        public RazorView(object model) {
            _model = model;
        }
    }
}
