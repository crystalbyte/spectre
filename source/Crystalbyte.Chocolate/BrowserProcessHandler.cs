using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Projections;

namespace Crystalbyte.Chocolate
{
    internal sealed class BrowserProcessHandler : RefCountedNativeObject {
        private readonly AppDelegate _appDelegate;

        public BrowserProcessHandler(AppDelegate appDelegate) 
            : base(typeof(CefBrowserProcessHandler)) {
            _appDelegate = appDelegate;
            // TODO: Implement event handlers.
            MarshalToNative(new CefRenderProcessHandler {
                Base = DedicatedBase
            });
        }
    }
}
