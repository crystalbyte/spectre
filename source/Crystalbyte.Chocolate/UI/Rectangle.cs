#region Namespace Directives

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
                result = (result * 397) ^ Y;
                result = (result * 397) ^ Width;
                result = (result * 397) ^ Height;
                return result;
            }
        }
    }
}