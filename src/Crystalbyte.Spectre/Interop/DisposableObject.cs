#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.Diagnostics;

#endregion

namespace Crystalbyte.Spectre.Interop {
    public abstract class DisposableObject : IDisposable {
        private bool _isDisposed;

        #region IDisposable Members

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~DisposableObject() {
            Debug.WriteLine(string.Format("Finalizer called on Type: {0}. ", GetType()));
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing) {
            if (_isDisposed) {
                return;
            }

            if (disposing) {
                DisposeManaged();
            }

            DisposeNative();

            _isDisposed = true;
        }

        /// <summary>
        ///   Use this override to dispose managed components.
        /// </summary>
        protected virtual void DisposeManaged() {
            // override
        }

        /// <summary>
        ///   Use this override to call appropriate destructors and free native memory.
        /// </summary>
        protected virtual void DisposeNative() {
            // override
        }

        public bool IsDisposed {
            get { return _isDisposed; }
        }
    }
}
