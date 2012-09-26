using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Crystalbyte.Chocolate.Web.Mvc
{
    public static class AppDomainExtensions
    {
        public static void Config(this AppDomain domain) {
            var physicalDir = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase).DirectoryName;
            const string virtualDir = "/";
            domain.SetData(".appDomain", "*");
            domain.SetData(".appPath", physicalDir);
            domain.SetData(".appVPath", virtualDir);
            domain.SetData(".domainId", domain.FriendlyName);
            domain.SetData(".hostingVirtualPath", virtualDir);
            domain.SetData(".hostingInstallDir", HttpRuntime.AspInstallDirectory);                
        }
    }
}
