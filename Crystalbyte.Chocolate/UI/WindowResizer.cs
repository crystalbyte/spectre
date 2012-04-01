#region Namespace Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class WindowResizer {
        public void Resize(IntPtr handle, Rectangle bounds) {
            var hdwp = NativeMethods.BeginDeferWindowPos(1);
            hdwp = NativeMethods.DeferWindowPos(hdwp, handle, IntPtr.Zero, bounds.X, bounds.Y, bounds.Width,
                                                bounds.Height,
                                                WindowResizeFlags.NoZorder);
            var success = NativeMethods.EndDeferWindowPos(hdwp);
            if (success) {
                return;
            }
            throw new ChocolateException("error resizing window.");
        }

        #region Nested type: NativeMethods

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