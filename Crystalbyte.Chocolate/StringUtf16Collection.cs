#region Namespace Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate
{
    [DebuggerDisplay("Count = {Count}")]
    internal sealed class Utf16StringCollection : DisposableObject, IValueCollection<string>
    {
        public Utf16StringCollection()
        {
            NativeHandle = CefStringListClass.CefStringListAlloc();
        }

        internal IntPtr NativeHandle { get; private set; }

        #region IValueCollection<string> Members

        public int Count
        {
            get { return CefStringListClass.CefStringListSize(NativeHandle); }
        }

        public string this[int index]
        {
            get
            {
                string value;
                var success = TryGetValue(index, out value);
                if (!success) {
                    throw new InvalidOperationException("index out of bounds");
                }
                return value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new Utf16StringCollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int index, out string value)
        {
            if (index >= Count) {
                value = null;
                return false;
            }

            var nativeDestination = new StringUtf16();
            var result  = CefStringListClass.CefStringListValue(NativeHandle, index, nativeDestination.NativeHandle);
            var success = Convert.ToBoolean(result);
            if (!success) {
                value = null;
                return false;
            }
            value = nativeDestination.Text;
            nativeDestination.Free();
            return true;
        }

        public void Add(string value)
        {
            var nativeSource = new StringUtf16(value);
            CefStringListClass.CefStringListAppend(NativeHandle, nativeSource.NativeHandle);
        }

        public void Clear()
        {
            // TODO: Not sure if clear actually frees memory from its children or merely clears the list across the native boundary.
            CefStringListClass.CefStringListClear(NativeHandle);
        }

        #endregion

        protected override void DisposeNative() {
            CefStringListClass.CefStringListClear(NativeHandle);
        }

        #region Nested type: Utf16StringCollectionEnumerator

        private sealed class Utf16StringCollectionEnumerator : IEnumerator<string>
        {
            private readonly int _itemCount;
            private readonly Utf16StringCollection _list;
            private int _currentIndex = -1;

            public Utf16StringCollectionEnumerator(Utf16StringCollection list)
            {
                _list = list;
                _itemCount = list.Count;
            }

            #region IEnumerator<string> Members

            public void Dispose()
            {
                // nothing
            }

            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _itemCount;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public string Current
            {
                get
                {
                    string item;
                    var success = _list.TryGetValue(_currentIndex, out item);
                    if (!success) {
                        throw new IndexOutOfRangeException("unable to access string list field");
                    }
                    return item;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            #endregion
        }

        #endregion
    }
}