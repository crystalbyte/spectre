using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Crystalbyte.Chocolate.Web.Mvc
{
    public sealed class AspRuntime {
        static AspRuntime() {
            Current = new AspRuntime();
        }

        public static AspRuntime Current { get; private set; }

        public AspRequestProcessor CreateHost() {
            var name = Process.GetCurrentProcess().MainModule.FileName;
            var setup = new AppDomainSetup {
                ApplicationName = "Chocolate Asp Request Processor"
                //ConfigurationFile = string.Format(name, ".config"),
            };

            var evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
            var appId = Guid.NewGuid().ToString();
            var domain = AppDomain.CreateDomain(appId, evidence, setup);
            domain.Config();

            var assemblyName = typeof (AspRequestProcessor).Assembly.FullName;
            var typeName = typeof(AspRequestProcessor).FullName;

            Debug.Assert(typeName != null, "typeName != null");
            return (AspRequestProcessor) domain.CreateInstanceAndUnwrap(assemblyName, typeName);
        }
    }
}
