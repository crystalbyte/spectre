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
using Crystalbyte.Spectre.Threading;

#endregion

namespace Crystalbyte.Spectre {
    public static class Errors {
        public static void ThrowInvalidCrossThreadCall(DispatcherQueue correctQueue) {
            var message = string.Format("Method called on invalid thread, please invoke into the {0} queue.",
                                        correctQueue);
            throw new InvalidOperationException(message);
        }
    }
}
