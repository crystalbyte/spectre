#region Namespace Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class FrameCollection : IEnumerable<Frame> {
        private readonly Browser _browser;

        public FrameCollection(Browser browser) {
            _browser = browser;
        }

        public Frame this[string name] {
            get {
                var reflection = _browser.MarshalFromNative<CefBrowser>();
                var str = new StringUtf16(name);
                var function = (GetFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetFrame, typeof (GetFrameCallback));
                var handle = function(_browser.NativeHandle, str.NativeHandle);
                str.Free();
                return Frame.FromHandle(handle);
            }
        }

        public Frame this[int ident] {
            get {
                var reflection = _browser.MarshalFromNative<CefBrowser>();
                var function = (GetFrameByidentCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetFrameByident,
                                                                     typeof (GetFrameByidentCallback));
                var handle = function(_browser.NativeHandle, ident);
                return Frame.FromHandle(handle);
            }
        }

        public int Count {
            get {
                var reflection = _browser.MarshalFromNative<CefBrowser>();
                var function = (GetFrameCountCallback) Marshal.GetDelegateForFunctionPointer(reflection.GetFrameCount,
                                                                                             typeof (
                                                                                                 GetFrameCountCallback));
                return function(_browser.NativeHandle);
            }
        }

        #region IEnumerable<Frame> Members

        public IEnumerator<Frame> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        #endregion

        #region Nested type: FrameCollectionEnumerator

        private sealed class FrameCollectionEnumerator : IEnumerator<Frame> {
            #region IEnumerator<Frame> Members

            public Frame Current {
                get { throw new NotImplementedException(); }
            }

            public void Dispose() {
                throw new NotImplementedException();
            }

            object IEnumerator.Current {
                get { throw new NotImplementedException(); }
            }

            public bool MoveNext() {
                throw new NotImplementedException();
            }

            public void Reset() {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion
    }
}