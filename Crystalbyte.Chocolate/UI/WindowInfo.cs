#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    internal sealed class WindowInfo : Adapter {
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
                Width = target.Size.Width,
                Height = target.Size.Height
            });
        }

        public IntPtr WindowHandle {
            get {
                var reflection = MarshalFromNative<CefWindowInfo>();
                return reflection.Window;
            }
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}