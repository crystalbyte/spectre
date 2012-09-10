using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public sealed class CustomSchemesRegisteringEventArgs : EventArgs
    {
        public CustomSchemesRegisteringEventArgs() {
            SchemeDescriptors = new List<SchemeDescriptor>();
        }

        public IList<SchemeDescriptor> SchemeDescriptors { get; private set; }
    }
}
