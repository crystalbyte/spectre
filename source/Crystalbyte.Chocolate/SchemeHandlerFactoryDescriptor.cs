using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public sealed class SchemeHandlerFactoryDescriptor
    {
        public SchemeHandlerFactoryDescriptor(string schemeName, SchemeHandlerFactory factory)
            : this(schemeName, string.Empty, factory) { }

        public SchemeHandlerFactoryDescriptor(string schemeName, string domainName, SchemeHandlerFactory factory) {
            SchemeName = schemeName;
            DomainName = domainName;
            Factory = factory;
        }

        public string SchemeName { get; set; }
        public string DomainName { get; set; }
        public SchemeHandlerFactory Factory { get; set; }
    }
}
