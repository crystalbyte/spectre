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
