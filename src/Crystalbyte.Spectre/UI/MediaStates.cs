using System;

namespace Crystalbyte.Spectre.UI {
    [Flags]
    public enum MediaStates {
        None = 0,
        Error = 1 << 0,
        Paused = 1 << 1,
        Muted = 1 << 2,
        Loop = 1 << 3,
        CanSave = 1 << 4,
        HasAudio = 1 << 5,
        HasVideo = 1 << 6,
        ControlRootElement = 1 << 7,
        CanPrint = 1 << 8,
        CanRotate = 1 << 9
    }
}