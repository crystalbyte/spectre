using System;

namespace Crystalbyte.Chocolate {
    [Flags]
    public enum SchemeProperties : byte {
        Standard = 1,
        Local = 2,
        DisplayIsolated = 4
    }
}