using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Razor {
    public abstract class Tile {

        public string Render() {
            var builder = new StringBuilder();
            var className = Shape == Shape.Rectangle ? "rectangle" : "square";
            builder.AppendFormat("<div class=\"{0}\">", className);
            RenderContent(builder);
            builder.Append("</div>");
            return builder.ToString();
        }

        protected abstract void RenderContent(StringBuilder builder);
        public abstract Shape Shape { get; }
    }
}