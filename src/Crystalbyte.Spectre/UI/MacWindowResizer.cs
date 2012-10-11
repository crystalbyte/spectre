#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class MacWindowResizer : IWindowResizer{
        #region IWindowResizer implementation

        public void Resize(IntPtr handle, Rectangle bounds){
            // no need to resize manually on os x
        }

        #endregion
    }
}