#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

#endregion

namespace Crystalbyte.Chocolate {
    /// <summary>
    ///   This class is a manged access point for native objects.
    ///   This class does not manage the objects lifecycle thus not allocating any memory.
    /// </summary>
    [DebuggerDisplay("NativeHandle = {NativeHandle}, Type = GetType()")]
    public abstract class Adapter : DisposableObject {
        protected Adapter(Type nativeType, bool isRefCounted = false) {
            if (nativeType == null) {
                throw new ArgumentNullException("nativeType");
            }
            if (!nativeType.IsValueType) {
                throw new NotSupportedException("Native images must be value types.");
            }
            NativeType = nativeType;
            NativeSize = Marshal.SizeOf(nativeType);
            IsRefCounted = isRefCounted;
        }

        private IntPtr _nativeHandle;
        protected internal IntPtr NativeHandle {
            get { return _nativeHandle; }
            protected set {
                if (value != IntPtr.Zero && _nativeHandle != value) {
                    _nativeHandle = value;
                }
            }
        }

        protected internal int NativeSize { get; private set; }
        protected Type NativeType { get; private set; }
        public bool IsRefCounted { get; private set; }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            return obj is Adapter && Equals((Adapter) obj);
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero && IsRefCounted) {
                Reference.Decrement(NativeHandle);
            }
        }

        protected internal void MarshalToNative(object value) {
            Marshal.StructureToPtr(value, NativeHandle, false);
        }

        protected internal T MarshalFromNative<T>() where T : struct {
            return (T) Marshal.PtrToStructure(NativeHandle, NativeType);
        }

        public bool Equals(Adapter other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            return other.NativeHandle.Equals(NativeHandle)
                   && other.NativeSize == NativeSize;
        }

        public override int GetHashCode() {
            unchecked {
                var result = NativeHandle.GetHashCode();
                result = (result * 397) ^ (NativeType != null ? NativeType.GetHashCode() : 0);
                result = (result * 397) ^ NativeSize;
                return result;
            }
        }
    }
}