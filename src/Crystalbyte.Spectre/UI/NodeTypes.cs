using System;

namespace Crystalbyte.Spectre.UI {
    [Flags]
    public enum NodeTypes {
        None = 0,
        Page = 1 << 0,
        Frame = 1 << 1,
        Link = 1 << 2,
        Media = 1 << 3,
        Selection = 1 << 4,
        Editable = 1 << 5
    }
}