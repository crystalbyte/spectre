using System;

namespace Crystalbyte.Spectre.UI {
    [Flags]
    public enum EditStates {
        None = 0,
        CanUndo = 1 << 0,
        CanRedo = 1 << 1,
        CanCut  = 1 << 2,
        CanCopy = 1 << 3,
        CanPaste = 1 << 4,
        CanDelete = 1 << 5,
        CanSelectAll = 1 << 6,
        CanTranslate = 1 << 7
    }
}