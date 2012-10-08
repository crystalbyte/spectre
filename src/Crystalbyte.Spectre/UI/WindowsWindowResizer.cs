#region Copyright notice

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
using System.Security;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class WindowsWindowResizer : IWindowResizer {
        public void Resize(IntPtr handle, Rectangle bounds) {
            var hdwp = NativeMethods.BeginDeferWindowPos(1);
            hdwp = NativeMethods.DeferWindowPos(hdwp, handle, IntPtr.Zero, bounds.X, bounds.Y, bounds.Width,
                                                bounds.Height,
                                                WindowResizeFlags.NoZorder);
            var success = NativeMethods.EndDeferWindowPos(hdwp);
            if (success) {
                return;
            }
            throw new InvalidOperationException("error resizing window.");
        }

        #region Nested type: NativeMethods

        [SuppressUnmanagedCodeSecurity]
        private static class NativeMethods {
            [DllImport("user32.dll")]
            public static extern IntPtr BeginDeferWindowPos(int windowCount);

            [DllImport("user32.dll")]
            public static extern IntPtr DeferWindowPos(IntPtr hdwp, IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y,
                                                       int width, int height,
                                                       [MarshalAs(UnmanagedType.U4)] WindowResizeFlags flags);

            [DllImport("user32.dll")]
            public static extern bool EndDeferWindowPos(IntPtr hdwp);
        }

        #endregion

        #region Nested type: WindowResizeFlags

        [Flags]
        private enum WindowResizeFlags : uint {
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            NoActivate = 0x0010,
            NoCopyBits = 0x0100,
            NoMove = 0x0002,
            NoOwnerZOrder = 0x0200,
            NoRedraw = 0x0008,
            NoReposition = 0x0200,
            NoSendChanging = 0x0400,
            NoSize = 0x0001,
            NoZorder = 0x0004,
            ShowWindow = 0x0040
        }

        #endregion
    }
}