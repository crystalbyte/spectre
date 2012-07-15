﻿#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class WindowsWindowInfo : Adapter {
        private readonly bool _isOwned;

        public WindowsWindowInfo(IRenderTarget target)
            : base(typeof (WindowsCefWindowInfo)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new WindowsCefWindowInfo {
                ParentWindow = target.Handle,
                Style = (uint) (WindowStyles.ChildWindow
                                | WindowStyles.ClipChildren
                                | WindowStyles.ClipSiblings
                                | WindowStyles.TabStop
                                | WindowStyles.Visible),
                X = 0,
                Y = 0,
                Width = target.Size.Width - Offsets.WindowRight,
                Height = target.Size.Height - Offsets.WindowBottom
            });
            _isOwned = true;
        }

        private WindowsWindowInfo(IntPtr handle)
            : base(typeof (WindowsCefWindowInfo)) {
            NativeHandle = handle;
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<WindowsCefWindowInfo>();
                return reflection.Window;
            }
        }

        public static WindowsWindowInfo FromHandle(IntPtr handle) {
            return new WindowsWindowInfo(handle);
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}