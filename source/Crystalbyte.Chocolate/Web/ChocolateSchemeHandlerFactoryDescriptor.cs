using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Web
{
    public sealed class ChocolateSchemeHandlerFactoryDescriptor : ISchemeHandlerFactoryDescriptor {

        private readonly ChocolateSchemeHandlerFactory _factory;

        public ChocolateSchemeHandlerFactoryDescriptor() {
            _factory = new ChocolateSchemeHandlerFactory();
        }

        public string SchemeName
        {
            get { return Schemes.Chocolate; }
        }

        public string DomainName
        {
            get { return "localhost"; }
        }

        public SchemeHandlerFactory Factory
        {
            get { return _factory; }
        }

        public IList<Type> RequestHandlerTypes
        {
            get { return _factory.RequestHandlerTypes; } 
        }
    }
}
