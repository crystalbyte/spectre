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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre {
    [DebuggerDisplay("Count = {Count}")]
    public sealed class StringUtf16List : NativeObject, IValueCollection<string> {

		private StringUtf16List (IntPtr handle) 
			: base(handle) { }

        public StringUtf16List() 
			: this(CefStringListClass.CefStringListAlloc()) { }

		public static StringUtf16List FromHandle(IntPtr handle) {
			return new StringUtf16List(handle);
		}

        #region IValueCollection<string> Members

        public int Count {
            get { return CefStringListClass.CefStringListSize(Handle); }
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
            return new StringUtf16ListEnumerator(this);
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
            var result = CefStringListClass.CefStringListValue(Handle, index, nativeDestination.Handle);
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
            CefStringListClass.CefStringListAppend(Handle, nativeSource.Handle);
        }

        public void Clear() {
            // TODO: Not sure if clear actually frees memory from its children or merely clears the list across the native boundary.
            CefStringListClass.CefStringListClear(Handle);
        }

        #endregion

        protected override void DisposeNative() {
            CefStringListClass.CefStringListClear(Handle);
        }

        #region Nested type: StringUtf16CollectionEnumerator

        private sealed class StringUtf16ListEnumerator : IEnumerator<string> {
            private readonly int _itemCount;
            private readonly StringUtf16List _list;
            private int _currentIndex = -1;

            public StringUtf16ListEnumerator(StringUtf16List list) {
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
