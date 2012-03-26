using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.UI {
    public sealed class AppContext {
        public AppContext(IRenderTarget target, ViewDelegate @delegate) {
            Initialize(target, @delegate, AppSettings.Default);
        }

        public AppContext(IRenderTarget target, ViewDelegate @delegate, AppSettings settings) {
            Initialize(target, @delegate, settings);
        }

        private void Initialize(IRenderTarget target, ViewDelegate @delegate, AppSettings settings) {
            RenderTarget = target;
            Settings = settings;
            Delegate = @delegate;
        }

        public IRenderTarget RenderTarget { get; private set; }
        public AppSettings Settings { get; private set; }
        public ViewDelegate Delegate { get; private set; }
    }
}
