#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class SizeChangedEventArgs : EventArgs {
        public SizeChangedEventArgs(Size size) {
            Size = size;
        }

        public Size Size { get; private set; }
    }
}