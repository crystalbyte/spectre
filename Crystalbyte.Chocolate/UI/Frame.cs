﻿#region Namespace Directives

using System;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class Frame : Adapter {
        private Frame(IntPtr handle)
            : base(typeof (CefFrame), true) {
            NativeHandle = handle;
        }

        public static Frame FromHandle(IntPtr handle) {
            return new Frame(handle);
        }
    }
}