using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class DialogResult {
        public DialogResult() {
            Message = string.Empty;
        }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
