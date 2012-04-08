using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;

namespace Crystalbyte.Chocolate.UI
{
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
                    Marshal.GetDelegateForFunctionPointer(reflection.GetFrame, typeof(GetFrameCallback));
                var handle = function(_browser.NativeHandle, str.NativeHandle);
                str.Free();
                return Frame.FromHandle(handle);
            }
        }

        public Frame this[int ident] {
            get {
                var reflection = _browser.MarshalFromNative<CefBrowser>();
                var function = (GetFrameByidentCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetFrameByident, typeof(GetFrameByidentCallback));
                var handle = function(_browser.NativeHandle, ident);
                return Frame.FromHandle(handle);
            }
        }

        public int Count {
            get {
                var reflection = _browser.MarshalFromNative<CefBrowser>();
                var function = (GetFrameCountCallback) Marshal.GetDelegateForFunctionPointer(reflection.GetFrameCount,
                                                                   typeof (GetFrameCountCallback));
                return function(_browser.NativeHandle);
            }
        }

        public IEnumerator<Frame> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private sealed class FrameCollectionEnumerator : IEnumerator<Frame> {

            public Frame Current
            {
                get { throw new NotImplementedException(); }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            object System.Collections.IEnumerator.Current
            {
                get { throw new NotImplementedException(); }
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
