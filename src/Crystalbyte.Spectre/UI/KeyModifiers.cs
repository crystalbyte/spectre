using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.UI {
    public enum KeyModifiers {
        None = 0,
        CapsLockDown = 1 << 0,
        ShiftDown = 1 << 1,
        ControlDown = 1 << 2,
        AltDown = 1 << 3,
        LeftMouseButton = 1 << 4,
        MiddleMouseButton = 1 << 5,
        RightMouseButton = 1 << 6,
        CommandDown = 1 << 7,
        Extended = 1 << 8,
    }
}
