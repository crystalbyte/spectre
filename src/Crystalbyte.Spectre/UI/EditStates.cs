#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
