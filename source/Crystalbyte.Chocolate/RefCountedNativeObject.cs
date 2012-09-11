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
using Crystalbyte.Chocolate.Projections;

#endregion

namespace Crystalbyte.Chocolate {
    /// <summary>
    ///   This class serves as a managed access point to a native object.
    ///   This class does manage the objects lifecycle allocating unmanaged memory on construction.
    ///   Memory is released once the reference counter reaches zero.
    /// </summary>
    public abstract class RefCountedNativeObject : NativeObject {
        private readonly ReleaseCallback _decrementDelegate;
        private readonly CefBase _dedicatedBase;
        private readonly GetRefctCallback _getRefCountDelegate;
        private readonly AddRefCallback _incrementDelegate;
        private readonly object _mutex;
        private int _referenceCounter;

        protected RefCountedNativeObject(Type nativeType)
            : base(nativeType, true) {
            _mutex = new object();
            _referenceCounter = 1;
            _incrementDelegate = Increment;
            _decrementDelegate = Decrement;
            _getRefCountDelegate = GetReferenceCount;
            _dedicatedBase = CreateBase(NativeSize);
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
        }

        internal CefBase DedicatedBase {
            get { return _dedicatedBase; }
        }

        internal int ReferenceCount {
            get { return _referenceCounter; }
        }

        internal void Increment() {
            Increment(NativeHandle);
        }

        internal void Decrement() {
            Decrement(NativeHandle);
        }

        private int Increment(IntPtr self) {
            VerifyHandle(self);
            lock (_mutex) {
                _referenceCounter++;
                return _referenceCounter;
            }
        }

        protected override void DisposeNative() {
            base.DisposeNative();
            if (_referenceCounter != 0) {
                return;
            }
        }

        private int Decrement(IntPtr self) {
            VerifyHandle(self);
            int refCount;
            lock (_mutex) {
                refCount = _referenceCounter--;
                if (refCount < 1) {
                    Free();
                }
            }
            return refCount;
        }

        private void Free() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
        }

        private int GetReferenceCount(IntPtr self) {
            VerifyHandle(self);
            lock (_mutex) {
                return _referenceCounter;
            }
        }

        private void VerifyHandle(IntPtr handle) {
            if (handle != NativeHandle) {
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