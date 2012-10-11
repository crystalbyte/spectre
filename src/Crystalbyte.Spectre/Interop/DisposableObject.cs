#region Using directives

using System;
using System.Diagnostics;

#endregion

namespace Crystalbyte.Spectre.Interop{
    public abstract class DisposableObject : IDisposable{
        private bool _isDisposed;

        #region IDisposable Members

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~DisposableObject(){
            Debug.WriteLine(string.Format("Finalizer called on Type: {0}. ", GetType()));
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing){
            if (_isDisposed){
                return;
            }

            if (disposing){
                DisposeManaged();
            }

            DisposeNative();

            _isDisposed = true;
        }

        /// <summary>
        ///   Use this override to dispose managed components.
        /// </summary>
        protected virtual void DisposeManaged(){
            // override
        }

        /// <summary>
        ///   Use this override to call appropriate destructors and free native memory.
        /// </summary>
        protected virtual void DisposeNative(){
            // override
        }
    }
}