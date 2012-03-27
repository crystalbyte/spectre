using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
    internal sealed class ClientHandler : CountedAdapter {
        private readonly ViewDelegate _delegate;
        public ClientHandler(ViewDelegate @delegate) 
            : base(typeof(CefClient)) {
                _delegate = @delegate;
                MarshalToNative(new CefClient {
                    Base = DedicatedBase
                });
        }
    }
}
