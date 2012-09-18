using System;
using System.Collections.Generic;
using Crystalbyte.Chocolate.Razor;

namespace Crystalbyte.Chocolate.Models
{
    [Serializable]
    public sealed class DesktopModel
    {
        public DesktopModel() {
            Tiles = new List<Tile> {
                new ImageTile("pack://siteoforigin:,,,/Media/Images/calendar.png", "Keep track of important events.")
            };
        }

        public IEnumerable<Tile> Tiles { get; private set; }
    }
}
