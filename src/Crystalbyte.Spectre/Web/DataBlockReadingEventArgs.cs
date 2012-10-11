#region Using directives

using System;
using System.IO;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class DataBlockReadingEventArgs : EventArgs{
        internal DataBlockReadingEventArgs(BinaryWriter writer){
            ResponseWriter = writer;
        }

        public int MaxBlockSize { get; internal set; }
        public BinaryWriter ResponseWriter { get; private set; }
        public ResponseDelayController DelayController { get; internal set; }
        public bool IsCompleted { get; set; }
    }
}