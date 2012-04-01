#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public interface IRenderTarget {
        Size Size { get; }
        IntPtr Handle { get; }
        Uri StartupUri { get; }
        void Show();
        event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
        event EventHandler TargetClosing;
        event EventHandler TargetClosed;
    }
}