#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class LinuxWindowResizer : IWindowResizer{
        #region IWindowResizer implementation

        public void Resize(IntPtr handle, Rectangle bounds){}

        #endregion
    }
}