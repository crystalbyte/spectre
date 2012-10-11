#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class SpectreSchemeHandlerFactoryDescriptor : ISchemeHandlerFactoryDescriptor{
        private readonly SpectreSchemeHandlerFactory _factory;

        public SpectreSchemeHandlerFactoryDescriptor(){
            _factory = new SpectreSchemeHandlerFactory();
        }

        #region ISchemeHandlerFactoryDescriptor Members

        public string SchemeName{
            get { return Schemes.Spectre; }
        }

        public string DomainName{
            get { return "localhost"; }
        }

        public SchemeHandlerFactory Factory{
            get { return _factory; }
        }

        #endregion

        public void Register(Type type){
            _factory.Providers.Register(type);
        }
    }
}