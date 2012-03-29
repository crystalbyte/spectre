﻿#region Namespace Directives

using System;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public interface IRenderTarget {
        IntPtr Handle { get; }
        Size StartSize { get; }
        Uri StartupUri { get; }
        event EventHandler<SizeChangedEventArgs> TargetSizeChanged;
        event EventHandler TargetClosing;
        event EventHandler TargetClosed;
    }
}