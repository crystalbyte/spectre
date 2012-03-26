using System;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class View : DisposableObject {
        private Browser _browser;
        private AppContext _context;

        public View(AppContext context) {
            _context = context;
        }

        
        public IRenderTarget RenderTarget { get; private set; }
        public ViewDelegate Delegate { get; private set; }
    }
}
