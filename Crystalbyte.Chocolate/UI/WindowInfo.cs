#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class WindowInfo : Adapter {
        private readonly bool _isOwned;

        public WindowInfo(IRenderTarget target)
            : base(typeof (CefWindowInfo)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefWindowInfo {
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

        private WindowInfo(IntPtr handle)
            : base(typeof (CefWindowInfo)) {
            NativeHandle = handle;
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<CefWindowInfo>();
                return reflection.Window;
            }
        }

        public static WindowInfo FromHandle(IntPtr handle) {
            return new WindowInfo(handle);
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}