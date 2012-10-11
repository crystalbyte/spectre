#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class CreateHandlerEventArgs : EventArgs{
        internal CreateHandlerEventArgs(){}

        public Browser Browser { get; internal set; }
        public Frame Frame { get; internal set; }
        public string Scheme { get; set; }
        // TODO: Add request to type
    }
}