#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

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
