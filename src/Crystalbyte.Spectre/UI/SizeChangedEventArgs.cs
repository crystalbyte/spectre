#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class SizeChangedEventArgs : EventArgs{
        public SizeChangedEventArgs(Size size){
            Size = size;
        }

        public Size Size { get; private set; }
    }
}