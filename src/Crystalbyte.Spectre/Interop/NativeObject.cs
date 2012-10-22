#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Interop {
    /// <summary>
    ///   This class is a managed access point for native cef objects.
    /// </summary>
    public abstract class NativeObject : DisposableObject {
        private IntPtr _nativeHandle;

        protected NativeObject(Type nativeType) {
            if (nativeType == null) {
                throw new ArgumentNullException("nativeType");
            }
            if (!nativeType.IsValueType) {
                throw new NotSupportedException("Native images must be value types.");
            }
            NativeType = nativeType;
            NativeSize = Marshal.SizeOf(nativeType);
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

        protected internal void MarshalToNative(object value) {
            Marshal.StructureToPtr(value, NativeHandle, false);
        }

        protected internal T MarshalFromNative<T>() where T : struct {
            return (T) Marshal.PtrToStructure(NativeHandle, NativeType);
        }
    }
}
