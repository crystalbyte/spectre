using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Razor
{
    public sealed class ImageTile : Tile {

        private readonly string _image;
        private readonly string _description;

        public ImageTile(string image, string description) {
            _image = image;
            _description = description;
        }

        protected override void RenderContent(StringBuilder builder) {
            builder.AppendFormat("<img src=\"{0}\" alt=\"{1}\" />", _image, _description);
        }

        public override Shape Shape
        {
            get { return Shape.Square; }
        }
    }
}
