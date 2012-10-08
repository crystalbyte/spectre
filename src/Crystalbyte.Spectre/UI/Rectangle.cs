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
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Chocolate.UI {
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