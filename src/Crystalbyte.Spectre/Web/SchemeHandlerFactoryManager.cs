#region Using directives

using System;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class SchemeHandlerFactoryManager{
        internal SchemeHandlerFactoryManager(){}

        public void Register(ISchemeHandlerFactoryDescriptor descriptor){
            if (descriptor == null){
                throw new ArgumentNullException("descriptor");
            }

            var s = new StringUtf16(descriptor.SchemeName);
            var d = new StringUtf16(descriptor.DomainName);

            CefSchemeCapi.CefRegisterSchemeHandlerFactory(s.NativeHandle, d.NativeHandle,
                                                          descriptor.Factory.NativeHandle);

            d.Free();
            s.Free();
        }

        public static void Clear(){
            CefSchemeCapi.CefClearSchemeHandlerFactories();
        }
    }
}