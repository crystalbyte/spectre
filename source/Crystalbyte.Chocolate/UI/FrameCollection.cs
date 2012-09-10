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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;

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
                var function = (GetFrameCountCallback)
                               Marshal.GetDelegateForFunctionPointer(reflection.GetFrameCount,
                                                                     typeof (GetFrameCountCallback));
                return function(_browser.NativeHandle);
            }
        }

        #region IEnumerable<Frame> Members

        public IEnumerator<Frame> GetEnumerator() {
            return new FrameCollectionEnumerator(this, _browser);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Nested type: FrameCollectionEnumerator

        private sealed class FrameCollectionEnumerator : IEnumerator<Frame> {
            private readonly FrameCollection _collection;
            private readonly int _count;
            private readonly IValueCollection<string> _frameNames;
            private int _index;

            public FrameCollectionEnumerator(FrameCollection collection, Browser browser) {
                _frameNames = (IValueCollection<string>) browser.FrameNames;
                _count = collection.Count;
                _collection = collection;
                _index = -1;
            }

            #region IEnumerator<Frame> Members

            public Frame Current {
                get {
                    var name = _frameNames[_index];
                    return _collection[name];
                }
            }

            public void Dispose() {
                // nada
            }

            object IEnumerator.Current {
                get { return Current; }
            }

            public bool MoveNext() {
                _index++;
                return _index < _count;
            }

            public void Reset() {
                _index = -1;
            }

            #endregion
        }

        #endregion
    }
}