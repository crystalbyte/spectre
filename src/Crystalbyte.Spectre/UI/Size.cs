#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI{
    [Serializable]
    public struct Size : IEquatable<Size>{
        public static readonly Size Empty = new Size(0, 0);
        private readonly int _height;
        private readonly int _width;

        public Size(int width, int height){
            _width = width;
            _height = height;
        }

        public bool IsEmpty{
            get { return Equals(Empty); }
        }

        public int Width{
            get { return _width; }
        }

        public int Height{
            get { return _height; }
        }

        #region IEquatable<Size> Members

        public bool Equals(Size other){
            return other._width == _width && other._height == _height;
        }

        #endregion

        public override bool Equals(object obj){
            if (ReferenceEquals(null, obj)){
                return false;
            }
            return obj is Size && Equals((Size) obj);
        }

        public override int GetHashCode(){
            unchecked{
                return (_width*397) ^ _height;
            }
        }
    }
}