#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    public interface IRenderTarget{
        Size Size { get; }
        IntPtr Handle { get; }
        Uri StartupUri { get; }
        void Show();
        event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
        event EventHandler TargetClosing;
        event EventHandler TargetClosed;
    }
}