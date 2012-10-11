#region Using directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Interop{
    /// <summary>
    ///   This class is a managed access point for native cef objects.
    ///   This class does not manage the object's lifecycle, thus not allocating any memory.
    /// </summary>
    public abstract class NativeObject : DisposableObject{
        private IntPtr _nativeHandle;

        protected NativeObject(Type nativeType){
            if (nativeType == null){
                throw new ArgumentNullException("nativeType");
            }
            if (!nativeType.IsValueType){
                throw new NotSupportedException("Native images must be value types.");
            }
            NativeType = nativeType;
            NativeSize = Marshal.SizeOf(nativeType);
        }

        protected internal IntPtr NativeHandle{
            get { return _nativeHandle; }
            protected set{
                if (value != IntPtr.Zero && _nativeHandle != value){
                    _nativeHandle = value;
                }
            }
        }

        protected internal int NativeSize { get; private set; }
        protected Type NativeType { get; private set; }

        protected internal void MarshalToNative(object value){
            Marshal.StructureToPtr(value, NativeHandle, false);
        }

        protected internal T MarshalFromNative<T>() where T : struct{
            return (T) Marshal.PtrToStructure(NativeHandle, NativeType);
        }
    }
}