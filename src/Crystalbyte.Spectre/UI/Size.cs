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
    public struct Size : IEquatable<Size> {
        public static readonly Size Empty = new Size(0, 0);
        private readonly int _height;
        private readonly int _width;

        public Size(int width, int height) {
            _width = width;
            _height = height;
        }

        public bool IsEmpty {
            get { return Equals(Empty); }
        }

        public int Width {
            get { return _width; }
        }

        public int Height {
            get { return _height; }
        }

        #region IEquatable<Size> Members

        public bool Equals(Size other) {
            return other._width == _width && other._height == _height;
        }

        #endregion

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            return obj is Size && Equals((Size) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_width*397) ^ _height;
            }
        }
    }
}
