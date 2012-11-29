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

#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.UI {
    [Flags]
    public enum EditStates {
        None = 0,
        CanUndo = 1 << 0,
        CanRedo = 1 << 1,
        CanCut = 1 << 2,
        CanCopy = 1 << 3,
        CanPaste = 1 << 4,
        CanDelete = 1 << 5,
        CanSelectAll = 1 << 6,
        CanTranslate = 1 << 7
    }
}
