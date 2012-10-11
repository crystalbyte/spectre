#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    internal interface IWindowResizer{
        void Resize(IntPtr handle, Rectangle bounds);
    }
}