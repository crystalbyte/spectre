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
