#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Web{
    [Flags]
    public enum SchemeProperties : byte{
        Standard = 1,
        Local = 2,
        DisplayIsolated = 4
    }
}