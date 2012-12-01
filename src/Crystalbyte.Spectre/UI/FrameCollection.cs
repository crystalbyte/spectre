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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class FrameCollection : IEnumerable<Frame> {
        private readonly Browser _browser;

        public FrameCollection(Browser browser) {
            _browser = browser;
        }

        public Frame this[string name] {
            get {
                var r = _browser.MarshalFromNative<CefBrowser>();
                var str = new StringUtf16(name);
                var function = (CefBrowserCapiDelegates.GetFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrame,
                                                                     typeof (CefBrowserCapiDelegates.GetFrameCallback));
                var handle = function(_browser.Handle, str.Handle);
                str.Free();
                return Frame.FromHandle(handle);
            }
        }

        public Frame this[int ident] {
            get {
                var r = _browser.MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetFrameByidentCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrameByident,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetFrameByidentCallback
                                                                         ));
                var handle = function(_browser.Handle, ident);
                return Frame.FromHandle(handle);
            }
        }

        public int Count {
            get {
                var r = _browser.MarshalFromNative<CefBrowser>();
                var function = (CefBrowserCapiDelegates.GetFrameCountCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrameCount,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetFrameCountCallback));
                return function(_browser.Handle);
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
