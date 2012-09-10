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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Crystalbyte.Chocolate.Projections.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    [DebuggerDisplay("Count = {Count}")]
    internal sealed class StringUtf16Collection : DisposableObject, IValueCollection<string> {
        public StringUtf16Collection() {
            NativeHandle = CefStringListClass.CefStringListAlloc();
        }

        internal IntPtr NativeHandle { get; private set; }

        #region IValueCollection<string> Members

        public int Count {
            get { return CefStringListClass.CefStringListSize(NativeHandle); }
        }

        public string this[int index] {
            get {
                string value;
                var success = TryGetValue(index, out value);
                if (!success) {
                    throw new InvalidOperationException("index out of bounds");
                }
                return value;
            }
        }

        public IEnumerator<string> GetEnumerator() {
            return new StringUtf16CollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool TryGetValue(int index, out string value) {
            if (index >= Count) {
                value = null;
                return false;
            }

            var nativeDestination = new StringUtf16();
            var result = CefStringListClass.CefStringListValue(NativeHandle, index, nativeDestination.NativeHandle);
            var success = Convert.ToBoolean(result);
            if (!success) {
                value = null;
                return false;
            }
            value = nativeDestination.Text;
            nativeDestination.Free();
            return true;
        }

        public void Add(string value) {
            var nativeSource = new StringUtf16(value);
            CefStringListClass.CefStringListAppend(NativeHandle, nativeSource.NativeHandle);
        }

        public void Clear() {
            // TODO: Not sure if clear actually frees memory from its children or merely clears the list across the native boundary.
            CefStringListClass.CefStringListClear(NativeHandle);
        }

        #endregion

        protected override void DisposeNative() {
            CefStringListClass.CefStringListClear(NativeHandle);
        }

        #region Nested type: StringUtf16CollectionEnumerator

        private sealed class StringUtf16CollectionEnumerator : IEnumerator<string> {
            private readonly int _itemCount;
            private readonly StringUtf16Collection _list;
            private int _currentIndex = -1;

            public StringUtf16CollectionEnumerator(StringUtf16Collection list) {
                _list = list;
                _itemCount = list.Count;
                _currentIndex = -1;
            }

            #region IEnumerator<string> Members

            public void Dispose() {
                // nothing
            }

            public bool MoveNext() {
                _currentIndex++;
                return _currentIndex < _itemCount;
            }

            public void Reset() {
                _currentIndex = -1;
            }

            public string Current {
                get {
                    string item;
                    var success = _list.TryGetValue(_currentIndex, out item);
                    if (!success) {
                        throw new IndexOutOfRangeException("unable to access string list field");
                    }
                    return item;
                }
            }

            object IEnumerator.Current {
                get { return Current; }
            }

            #endregion
        }

        #endregion
    }
}