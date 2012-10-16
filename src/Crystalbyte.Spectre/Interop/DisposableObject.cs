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
            //Debug.WriteLine(string.Format("Finalizer called on Type: {0}. ", GetType()));
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
    }
}
