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
