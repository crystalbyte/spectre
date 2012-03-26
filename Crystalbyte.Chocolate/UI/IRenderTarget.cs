#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public interface IRenderTarget {
        IntPtr Handle { get; }
        Uri StartupUri { get; }
        event EventHandler<SizeChangedEventArgs> SizeChanged;
        event EventHandler Closed;
    }
}