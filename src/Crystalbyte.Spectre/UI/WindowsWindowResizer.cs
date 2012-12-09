#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace Crystalbyte.Spectre.UI {
    internal sealed class WindowsWindowResizer : IWindowResizer {
        #region IWindowResizer Members

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

        #endregion

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
