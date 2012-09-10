using System;
using System.Collections.Generic;

namespace Crystalbyte.Chocolate.Mvc {
    public sealed class ViewGenerationFailedEventArgs : EventArgs {
        internal ViewGenerationFailedEventArgs() {
            
        }

        public IEnumerable<Exception> Errors { get; internal set; }
    }
}