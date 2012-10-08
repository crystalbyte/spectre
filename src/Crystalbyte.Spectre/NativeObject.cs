#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre {
    /// <summary>
    ///   This class is a managed access point for native cef objects.
    ///   This class does not manage the object's lifecycle, thus not allocating any memory.
    /// </summary>
    public abstract class NativeObject : DisposableObject {
        private IntPtr _nativeHandle;

        protected NativeObject(Type nativeType, bool isRefCounted = false) {
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
        protected bool IsRefCounted { get; private set; }

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
    }
}