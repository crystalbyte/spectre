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

#endregion

namespace Crystalbyte.Spectre.UI {
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
