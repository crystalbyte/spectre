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
using System.IO;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class DataBlockReadingEventArgs : EventArgs {
        internal DataBlockReadingEventArgs(BinaryWriter writer) {
            ResponseWriter = writer;
        }

        public int MaxBlockSize { get; internal set; }
        public BinaryWriter ResponseWriter { get; private set; }
        public CallbackObject DelayController { get; internal set; }
        public bool IsCompleted { get; set; }
    }
}
