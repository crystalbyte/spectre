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
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.UI {
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle : IEquatable<Rectangle> {
        public Rectangle(Point p, Size s)
            : this(p.X, p.Y, s.Width, s.Height) {}

        public Rectangle(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Rectangle FromHandle(IntPtr handle) {
            var rectangle = new Rectangle();
            Marshal.PtrToStructure(handle, rectangle);
            return rectangle;
        }

        public readonly int X;
        public readonly int Y;
        public readonly int Width;
        public readonly int Height;

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            return obj is Rectangle && Equals((Rectangle) obj);
        }

        public bool Equals(Rectangle other) {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
        }

        public override int GetHashCode() {
            unchecked {
                var result = X;
                result = (result*397) ^ Y;
                result = (result*397) ^ Width;
                result = (result*397) ^ Height;
                return result;
            }
        }
    }
}
