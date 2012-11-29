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
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Interop {
    /// <summary>
    ///   This class serves as a managed access point to a native object.
    ///   This class does manage the objects lifecycle allocating unmanaged memory on construction.
    ///   Memory is released once the reference counter reaches zero.
    /// </summary>
    public abstract class OwnedRefCountedCefTypeAdapter : RefCountedCefTypeAdapter {
        private readonly CefBaseCapiDelegates.ReleaseCallback _decrementDelegate;
        private readonly CefBaseCapiDelegates.GetRefctCallback _getRefCountDelegate;
        private readonly CefBaseCapiDelegates.AddRefCallback _incrementDelegate;
        private readonly CefBase _dedicatedBase;
        private readonly object _mutex;
        private int _referenceCounter;

        protected OwnedRefCountedCefTypeAdapter(Type nativeType)
            : base(nativeType) {

            _mutex = new object();
            _referenceCounter = 1;
            _incrementDelegate = Increment;
            _decrementDelegate = Decrement;
            _getRefCountDelegate = GetReferenceCount;
            _dedicatedBase = CreateBase(NativeSize);

            Handle = Marshal.AllocHGlobal(NativeSize);
        }

        internal CefBase DedicatedBase {
            get { return _dedicatedBase; }
        }

        internal int ReferenceCount {
            get { return _referenceCounter; }
        }

        internal void Increment() {
            Increment(Handle);
        }

        internal void Decrement() {
            Decrement(Handle);
        }

        private int Increment(IntPtr self) {
            VerifyHandle(self);
            lock (_mutex) {
                _referenceCounter++;
                return _referenceCounter;
            }
        }

        private int Decrement(IntPtr self) {
            VerifyHandle(self);
            int refCount;
            lock (_mutex) {
                refCount = _referenceCounter++;
                if (refCount < 1) {
                    Free();
                }
            }
            return refCount;
        }

        private void Free() {
            if (Handle != IntPtr.Zero) {
                Marshal.FreeHGlobal(Handle);
            }
        }

        private int GetReferenceCount(IntPtr self) {
            VerifyHandle(self);
            lock (_mutex) {
                return _referenceCounter;
            }
        }

        private void VerifyHandle(IntPtr handle) {
            if (handle != Handle) {
                throw new InvalidOperationException("Ref count handle is invalid.");
            }
        }

        private CefBase CreateBase(int size) {
            return new CefBase {
                Size = size,
                AddRef =
                    Marshal.GetFunctionPointerForDelegate(_incrementDelegate),
                Release =
                    Marshal.GetFunctionPointerForDelegate(_decrementDelegate),
                GetRefct =
                    Marshal.GetFunctionPointerForDelegate(_getRefCountDelegate)
            };
        }
    }
}
