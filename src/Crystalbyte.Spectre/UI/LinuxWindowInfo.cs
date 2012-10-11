#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class LinuxWindowInfo : NativeObject{
        private readonly bool _isOwned;

        public LinuxWindowInfo(IRenderTarget target)
            : base(typeof (LinuxCefWindowInfo)){
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new LinuxCefWindowInfo{
                                                      ParentWidget = target.Handle
                                                  });
            _isOwned = true;
        }

        protected override void DisposeNative(){
            if (NativeHandle != IntPtr.Zero && _isOwned){
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}