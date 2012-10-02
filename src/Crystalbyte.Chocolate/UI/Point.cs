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

#endregion

namespace Crystalbyte.Chocolate.UI {
    [Serializable]
    public struct Point : IEquatable<Point> {
        public static Point Empty = new Point(0, 0);
        private readonly int _x;
        private readonly int _y;

        public Point(int xy)
            : this(xy, xy) {}

        public Point(int x, int y) {
            _x = x;
            _y = y;
        }

        public int X {
            get { return _x; }
        }

        public int Y {
            get { return _y; }
        }

        public bool IsEmpty {
            get { return Equals(Empty); }
        }

        #region IEquatable<Point> Members

        public bool Equals(Point other) {
            return other._x == _x && other._y == _y;
        }

        #endregion

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            return obj is Point && Equals((Point) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_x*397) ^ _y;
            }
        }
    }
}