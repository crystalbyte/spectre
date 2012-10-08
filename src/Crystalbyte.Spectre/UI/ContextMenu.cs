using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ContextMenu : NativeObject {
        public ContextMenu() 
            : base(typeof(CefMenuModel), true) {
            
        }
    }
}
