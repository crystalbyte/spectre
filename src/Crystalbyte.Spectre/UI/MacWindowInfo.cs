#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class MacWindowInfo : NativeObject{
        private readonly bool _isOwned;

        public MacWindowInfo(IRenderTarget target)
            : base(typeof (MacCefWindowInfo)){
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new MacCefWindowInfo{
                                                    ParentView = target.Handle,
                                                    X = 0,
                                                    Y = 0,
                                                    Width = target.Size.Width,
                                                    Height = target.Size.Height
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