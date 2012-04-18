using System;
using Crystalbyte.Chocolate.UI;
using Crystalbyte.Chocolate.Bindings.Internal;
using System.Runtime.InteropServices;

namespace Crystalbyte.Chocolate
{
	public sealed class MacWindowInfo : Adapter
	{
		private readonly bool _isOwned;
		
		public MacWindowInfo (IRenderTarget target) 
			: base(typeof(MacCefWindowInfo)) {
			
			NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new MacCefWindowInfo {
                ParentView = target.Handle,
                X = 0,
                Y = 0,
                Width = target.Size.Width, // - Offsets.WindowRight,
                Height = target.Size.Height // - Offsets.WindowBottom
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

