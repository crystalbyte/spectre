using System;
using Crystalbyte.Chocolate.UI;
using Crystalbyte.Chocolate.Bindings.Internal;
using System.Runtime.InteropServices;

namespace Crystalbyte.Chocolate
{
	public sealed class LinuxWindowInfo : Adapter {
        private readonly bool _isOwned;

        public LinuxWindowInfo(IRenderTarget target)
            : base(typeof (LinuxCefWindowInfo)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new LinuxCefWindowInfo {
                ParentWidget = target.Handle
            });
            _isOwned = true;
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero && _isOwned) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
	}
}

