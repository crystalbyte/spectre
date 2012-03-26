#region Namespace Directives

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
                return (_x * 397) ^ _y;
            }
        }
    }
}