using System;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI {
    internal sealed class GeolocationHandler : OwnedAdapter {
        private BrowserDelegate _delegate;
        public GeolocationHandler(BrowserDelegate @delegate) 
            : base(typeof(CefGeolocationHandler)) {
            _delegate = @delegate;
            MarshalToNative(new CefGeolocationHandler {
                Base = DedicatedBase
            });
        }
    }
}