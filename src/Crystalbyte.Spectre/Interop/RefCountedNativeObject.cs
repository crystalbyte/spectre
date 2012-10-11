#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Interop{
    public abstract class RefCountedNativeObject : NativeObject{
        protected RefCountedNativeObject(Type nativeType)
            : base(nativeType){}

        protected override void DisposeNative(){
            if (NativeHandle != IntPtr.Zero){
                Reference.Decrement(NativeHandle);
            }
        }
    }
}