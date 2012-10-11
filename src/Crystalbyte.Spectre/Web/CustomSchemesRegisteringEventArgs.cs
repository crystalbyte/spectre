#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class CustomSchemesRegisteringEventArgs : EventArgs{
        public CustomSchemesRegisteringEventArgs(){
            SchemeDescriptors = new List<ISchemeDescriptor>();
        }

        public IList<ISchemeDescriptor> SchemeDescriptors { get; private set; }
    }
}